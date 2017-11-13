using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using static PLS.SKS.Package.BusinessLogic.Validator;
using PLS.SKS.Package;
using Microsoft.Extensions.DependencyInjection;

namespace PLS.SKS.Package.BusinessLogic
{
    public class HopArrivalLogic : Interfaces.IHopArrivalLogic
    {
		private DataAccess.Interfaces.IParcelRepository parcelRepo;

		public HopArrivalLogic(IServiceProvider serviceProvider)
		{
			parcelRepo = new DataAccess.Sql.SqlParcelRepository(serviceProvider.GetRequiredService<DataAccess.Sql.DBContext>());
		}

		public void scanParcel(string trackingNumber, string code)
        {
			//TrackingInformation des Parcels gehört geupdated
			//Was mach ich mit dem HopArrival?
			//DataAccess.Entities.Parcel dalParcel = parcelRepo.GetByCode(trackingNumber);
		}
	}
}
