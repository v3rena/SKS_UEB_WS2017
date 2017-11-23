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
        private DataAccess.Interfaces.ITrackingInformationRepository trackRepo;
        private DataAccess.Interfaces.IHopArrivalRepository hopRepo;

		public HopArrivalLogic(IServiceProvider serviceProvider)
		{
            parcelRepo = new DataAccess.Sql.SqlParcelRepository(serviceProvider.GetRequiredService<DataAccess.Sql.DBContext>());
            trackRepo = new DataAccess.Sql.SqlTrackingInformationRepository(serviceProvider.GetRequiredService<DataAccess.Sql.DBContext>());
            hopRepo = new DataAccess.Sql.SqlHopArrivalRepository(serviceProvider.GetRequiredService<DataAccess.Sql.DBContext>());
        }

		public void ScanParcel(string trackingNumber, string code)
        {



            //get Parcel with trackingNumber
            //DataAccess.Entities.Parcel dalParcel = parcelRepo.GetByTrackingNumber(trackingNumber);
            //get TrackingInformation for Parcel
            //DataAccess.Entities.TrackingInformation dalInfo = trackRepo.GetById(dalParcel.TrackingInformationId);
            //get HopArrival with "code" and "TrackingInformationID"
            //DataAccess.Entities.HopArrival hopArr = hopRepo.
            //change state of HopArrival



            //TrackingInformation des Parcels gehört geupdated
            //Was mach ich mit dem HopArrival?
            //DataAccess.Entities.Parcel dalParcel = parcelRepo.GetByCode(trackingNumber);
        }
    }
}
