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
		private DataAccess.Interfaces.IParcelRepository parcelRepo;

		public TrackingLogic(IParcelRepository parcelRepository)
		{
			parcelRepo = parcelRepository;
		}

		public DataAccess.Entities.Parcel TrackParcel(string trackingNumber)
        {
            return parcelRepo.GetByTrackingNumber(trackingNumber);
		}
    }
}
