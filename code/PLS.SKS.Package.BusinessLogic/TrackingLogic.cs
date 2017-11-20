﻿using Microsoft.Extensions.DependencyInjection;
using PLS.SKS.Package.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic
{
    public class TrackingLogic : Interfaces.ITrackingLogic
    {
		private DataAccess.Interfaces.IParcelRepository parcelRepo;

		public TrackingLogic(IServiceProvider serviceProvider)
		{
			parcelRepo = new DataAccess.Sql.SqlParcelRepository(serviceProvider.GetRequiredService<DataAccess.Sql.DBContext>());
		}

        public DataAccess.Entities.Parcel trackParcel(string trackingNumber)
        {
            //get Parcel with TrackingNumber
            //get TrackingInformation of Parcel
            //get all Hops of TrackingInformation
            //fill lists with corresponding Hops
            //return DAL Parcel

            return parcelRepo.GetByTrackingNumber(trackingNumber);
		}
    }
}
