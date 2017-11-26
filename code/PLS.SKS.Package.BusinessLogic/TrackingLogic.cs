using Microsoft.Extensions.DependencyInjection;
using PLS.SKS.Package.BusinessLogic.Entities;
using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic
{
    public class TrackingLogic : Interfaces.ITrackingLogic
    {
		private DataAccess.Interfaces.IParcelRepository parcelRepo;
        private DataAccess.Interfaces.IHopArrivalRepository hopRepo;

		public TrackingLogic(IParcelRepository parcelRepository, IHopArrivalRepository hopRepository)
		{
			parcelRepo = parcelRepository;
            hopRepo = hopRepository;
		}

		public DataAccess.Entities.Parcel TrackParcel(string trackingNumber)
        {
            DataAccess.Entities.Parcel DALParcel = parcelRepo.GetByTrackingNumber(trackingNumber);
            //get HopArrivals with "TrackingInformationID"
            List<DataAccess.Entities.HopArrival> hopArr = hopRepo.GetByTrackingInformationId(DALParcel.TrackingInformationId);
            //fill visitedHops and futureHops lists
            foreach(var h in hopArr)
            {
                if(h.Status == "visited")
                {
                    DALParcel.TrackingInformation.visitedHops.Add(h);
                }
                else if(h.Status == "future")
                {
                    DALParcel.TrackingInformation.futureHops.Add(h);
                }
            }
            return DALParcel;
		}
    }
}
