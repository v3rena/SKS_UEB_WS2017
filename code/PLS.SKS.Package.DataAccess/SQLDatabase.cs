using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Interfaces;

namespace PLS.SKS.Package.DataAccess
{
    public class SQLDatabase : Interfaces.IDatabase
    {
        public SQLDatabase()
        {
            /*hopArrivalRepo = new Sql.SqlHopArrivalRepository();
            parcelRepo = new Sql.SqlParcelRepository();
            recipientRepo = new Sql.SqlRecipientRepository();
            trackingRepo = new Sql.SqlTrackingInformationRepository();
            truckRepo = new Sql.SqlTruckRepository();
            warehouseRepo = new Sql.SqlWarehouseRepository();*/
        }

        public IHopArrivalRepository hopArrivalRepo { get; }

        public IParcelRepository parcelRepo { get; }

        public IRecipientRepository recipientRepo { get; }

        public ITrackingInformationRepository trackingRepo { get; }

        public ITruckRepository truckRepo { get; }

        public IWarehouseRepository warehouseRepo { get; }


    }
}
