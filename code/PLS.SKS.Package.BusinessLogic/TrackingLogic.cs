using Microsoft.Extensions.DependencyInjection;
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
			//This is not cool, Db query should return parcel by tracking nr, not primary key
			int nr = Convert.ToInt32(trackingNumber);
			return parcelRepo.GetById(nr);
        }
    }
}
