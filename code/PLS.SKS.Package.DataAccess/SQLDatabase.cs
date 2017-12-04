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
            //hopArrivalRepo = new Sql.SqlHopArrivalRepository();
            //hopArrivalRepo = new Sql.SqlHopArrivalRepository();
            //parcelRepo = new Sql.SqlParcelRepository();
            //recipientRepo = new Sql.SqlRecipientRepository();
            //trackingRepo = new Sql.SqlTrackingInformationRepository();
            //truckRepo = new Sql.SqlTruckRepository();
            //warehouseRepo = new Sql.SqlWarehouseRepository();
        }

        public IHopArrivalRepository HopArrivalRepo { get; }

        public IParcelRepository ParcelRepo { get; }

        public IRecipientRepository RecipientRepo { get; }

        public ITrackingInformationRepository TrackingRepo { get; }

        public ITruckRepository TruckRepo { get; }

        public IWarehouseRepository WarehouseRepo { get; }


    }
}
