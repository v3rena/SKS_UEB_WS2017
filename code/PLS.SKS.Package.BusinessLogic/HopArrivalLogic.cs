using System;
using PLS.SKS.Package.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using PLS.SKS.Package.BusinessLogic.Helpers;

namespace PLS.SKS.Package.BusinessLogic
{
    public class HopArrivalLogic : Interfaces.IHopArrivalLogic
    {
		private readonly IParcelRepository _parcelRepo;
        private readonly ITrackingInformationRepository _trackingRepo;
        private readonly IHopArrivalRepository _hopArrivalRepo;
        private readonly ITruckRepository _truckRepository;
		private readonly ILogger<HopArrivalLogic> _logger;
		private readonly AutoMapper.IMapper _mapper;

		public HopArrivalLogic(IParcelRepository parcelRepository, ITrackingInformationRepository trackingInformationRepository, IHopArrivalRepository hopArrivalRepository, ITruckRepository truckRepository, ILogger<HopArrivalLogic> logger, AutoMapper.IMapper mapper)
		{
			_parcelRepo = parcelRepository;
			_trackingRepo = trackingInformationRepository;
			_hopArrivalRepo = hopArrivalRepository;
            _truckRepository = truckRepository;
			_logger = logger;
			_mapper = mapper;
		}

		public void ScanParcel(string trackingNumber, string code)
        {
            try
            {
                var dalParcel = _parcelRepo.GetByTrackingNumber(trackingNumber);
                if(dalParcel == null)
                {
                    throw new BlException("Parcel not found in Database");
                }
                var dalInfo = _trackingRepo.GetById(dalParcel.TrackingInformationId);
                var hopArr = _hopArrivalRepo.GetByTrackingInformationId(dalInfo.Id);

                int index = hopArr.FindIndex(a => a.Code == code);
                if (index == -1)
                {
                    throw new BlException("Wrong hop for parcel");
                }
                hopArr[index].Status = "visited";
                hopArr[index].DateTime = DateTime.Now;

                var trucks = _truckRepository.GetAll();
                foreach (var t in trucks)
                {
                    if(t.Code == hopArr[index].Code)
                    {
                        dalInfo.State = DataAccess.Entities.TrackingInformation.StateEnum.InTruckDeliveryEnum;
                        break;
                    }
                }

                _hopArrivalRepo.Update(hopArr[index]);
            }
			catch (Exception ex)
			{
				_logger.LogError("Could not update parcel information", ex);
				throw new BlException("Could not update parcel information", ex);
			}
		}
    }
}
