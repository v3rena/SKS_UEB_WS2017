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

		/*public TrackingLogic(IServiceProvider serviceProvider)
		{
			parcelRepo = new DataAccess.Sql.SqlParcelRepository(serviceProvider.GetRequiredService<DataAccess.Sql.DBContext>());
		}*/

		public TrackingLogic(IParcelRepository parcelRepository)
		{
			parcelRepo = parcelRepository;
		}

		public DataAccess.Entities.Parcel TrackParcel(string trackingNumber)
        {
			//futureHops and visitedHops are still empty!
            return parcelRepo.GetByTrackingNumber(trackingNumber);
		}
    }
}
