using PLS.SKS.Package.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic
{
    public class TrackingLogic : Interfaces.ITrackingLogic
    {
		DataAccess.Sql.SqlParcelRepository parcelRepo = new DataAccess.Sql.SqlParcelRepository();

		public TrackingLogic()
        {

        }

        public void trackParcel(string trackingID)
        {

			//return parcelRepo.GetById(trackingID);
        }
    }
}
