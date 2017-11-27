﻿using Microsoft.Extensions.DependencyInjection;
using PLS.SKS.Package.BusinessLogic.Entities;
using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic
{
    public class TrackingLogic : Interfaces.ITrackingLogic
    {
		private IParcelRepository parcelRepo;
        private IHopArrivalRepository hopRepo;
        private ITrackingInformationRepository trackRepo;

		public TrackingLogic(IParcelRepository parcelRepository, IHopArrivalRepository hopRepository, ITrackingInformationRepository trackRepository)
		{
			parcelRepo = parcelRepository;
            hopRepo = hopRepository;
            trackRepo = trackRepository;
		}

		public DataAccess.Entities.Parcel TrackParcel(string trackingNumber)
        {
            DataAccess.Entities.Parcel DALParcel = parcelRepo.GetByTrackingNumber(trackingNumber);


#if DEBUG
            //Fuer Tests
            if(DALParcel.TrackingInformation == null)
            {
                DALParcel.TrackingInformation = trackRepo.GetById(DALParcel.TrackingInformationId);
            }
#endif
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
