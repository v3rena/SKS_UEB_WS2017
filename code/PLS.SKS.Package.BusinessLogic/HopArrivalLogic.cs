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
            DataAccess.Entities.Parcel dalParcel = parcelRepo.GetByTrackingNumber(trackingNumber);
            //get TrackingInformation for Parcel
            DataAccess.Entities.TrackingInformation dalInfo = trackRepo.GetById(dalParcel.TrackingInformationId);
            //get HopArrivals with "TrackingInformationID"
            List<DataAccess.Entities.HopArrival> hopArr = hopRepo.GetByTrackingInforamtionId(dalInfo.Id);
            //get HopArrival with "Code"
            DataAccess.Entities.HopArrival h = new DataAccess.Entities.HopArrival { Code = code };
            int index = hopArr.FindIndex(a => a.Code == h.Code);
            //update Status to visited
            hopArr[index].Status = "visited";
            //update DateTime to now
            hopArr[index].DateTime = DateTime.Now;

            hopRepo.Update(hopArr[index]);

            //TrackingInformation des Parcels gehört geupdated
            //Was mach ich mit dem HopArrival?
            //DataAccess.Entities.Parcel dalParcel = parcelRepo.GetByCode(trackingNumber);
        }
    }
}
