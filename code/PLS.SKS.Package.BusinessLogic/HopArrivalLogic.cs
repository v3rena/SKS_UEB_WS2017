using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using static PLS.SKS.Package.BusinessLogic.Validator;
using PLS.SKS.Package;
using Microsoft.Extensions.DependencyInjection;
using PLS.SKS.Package.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;

namespace PLS.SKS.Package.BusinessLogic
{
    public class HopArrivalLogic : Interfaces.IHopArrivalLogic
    {
		private IParcelRepository parcelRepo;
        private ITrackingInformationRepository trackingRepo;
        private IHopArrivalRepository hopArrivalRepo;
		private ILogger<HopArrivalLogic> logger;
		private AutoMapper.IMapper mapper;

		public HopArrivalLogic(IParcelRepository parcelRepository, ITrackingInformationRepository trackingInformationRepository, IHopArrivalRepository hopArrivalRepository, ILogger<HopArrivalLogic> logger, AutoMapper.IMapper mapper)
		{
			parcelRepo = parcelRepository;
			trackingRepo = trackingInformationRepository;
			hopArrivalRepo = hopArrivalRepository;
			this.logger = logger;
			this.mapper = mapper;
		}

		public void ScanParcel(string trackingNumber, string code)
        {
			logger.LogInformation("Calling the ScanParcel action");
            try
            {
                DataAccess.Entities.Parcel dalParcel = parcelRepo.GetByTrackingNumber(trackingNumber);
                if(dalParcel == null)
                {
                    throw new BLException("Parcel not found in Database");
                }
                DataAccess.Entities.TrackingInformation dalInfo = trackingRepo.GetById(dalParcel.TrackingInformationId);
                List<DataAccess.Entities.HopArrival> hopArr = hopArrivalRepo.GetByTrackingInformationId(dalInfo.Id);

                DataAccess.Entities.HopArrival h = new DataAccess.Entities.HopArrival { Code = code };
                int index = hopArr.FindIndex(a => a.Code == h.Code);
                if (index == -1)
                {
                    throw new BLException("Wrong Hop for Parcel");
                }
                hopArr[index].Status = "visited";
                hopArr[index].DateTime = DateTime.Now;

                hopArrivalRepo.Update(hopArr[index]);
            }
			catch (Exception ex)
			{
				logger.LogError("Could not update parcel information", ex);
				throw new BLException("Could not update parcel information", ex);
			}
		}
    }
}
