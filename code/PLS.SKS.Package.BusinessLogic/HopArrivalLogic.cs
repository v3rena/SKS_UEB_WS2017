using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using static PLS.SKS.Package.BusinessLogic.Validator;
using PLS.SKS.Package;
using Microsoft.Extensions.DependencyInjection;
using PLS.SKS.Package.DataAccess.Interfaces;

namespace PLS.SKS.Package.BusinessLogic
{
    public class HopArrivalLogic : Interfaces.IHopArrivalLogic
    {
		private DataAccess.Interfaces.IParcelRepository parcelRepo;
        private DataAccess.Interfaces.ITrackingInformationRepository trackingRepo;
        private DataAccess.Interfaces.IHopArrivalRepository hopArrivalRepo;

		public HopArrivalLogic(IParcelRepository parcelRepository, ITrackingInformationRepository trackingInformationRepository, IHopArrivalRepository hopArrivalRepository)
		{
			parcelRepo = parcelRepository;
			trackingRepo = trackingInformationRepository;
			hopArrivalRepo = hopArrivalRepository;
		}

		public void ScanParcel(string trackingNumber, string code)
        {
            //get Parcel with trackingNumber
            DataAccess.Entities.Parcel dalParcel = parcelRepo.GetByTrackingNumber(trackingNumber);
            //get TrackingInformation for Parcel
            DataAccess.Entities.TrackingInformation dalInfo = trackingRepo.GetById(dalParcel.TrackingInformationId);
            //get HopArrivals with "TrackingInformationID"
            List<DataAccess.Entities.HopArrival> hopArr = hopArrivalRepo.GetByTrackingInformationId(dalInfo.Id);
            //get HopArrival with "Code"
            DataAccess.Entities.HopArrival h = new DataAccess.Entities.HopArrival { Code = code };
            int index = hopArr.FindIndex(a => a.Code == h.Code);
            //update Status to visited
            hopArr[index].Status = "visited";
            //update DateTime to now
            hopArr[index].DateTime = DateTime.Now;

			hopArrivalRepo.Update(hopArr[index]);
        }
    }
}
