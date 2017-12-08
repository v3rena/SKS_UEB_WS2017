using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Interfaces;

namespace PLS.SKS.Package.DataAccess
{
    public class MockDatabase : Interfaces.IDatabase
    {
        public MockDatabase()
        {
            _recRepo = new Mock.MockRecipientRepository();
            _trackingRepo = new Mock.MockTrackingInformationRepository();
            _truckRepo = new Mock.MockTruckRepository();
            _warehouseRepo = new Mock.MockWarehouseRepository();
            _parcelRepo = new Mock.MockParcelRepository(_trackingRepo);

        }

        IParcelRepository parcelRepo
        {
            get
            {
                return _parcelRepo;
            }
        }

        IHopArrivalRepository hopArrivalRepo
        {
            get
            {
				return _hopArrivalRepo;
            }
        }

        IRecipientRepository recipientRepo => throw new NotImplementedException();

        ITrackingInformationRepository trackingRepo => throw new NotImplementedException();

        ITruckRepository IDatabase.TruckRepo => throw new NotImplementedException();

        IWarehouseRepository IDatabase.WarehouseRepo => throw new NotImplementedException();

		IParcelRepository IDatabase.ParcelRepo => throw new NotImplementedException();

		IRecipientRepository IDatabase.RecipientRepo => throw new NotImplementedException();

		ITrackingInformationRepository IDatabase.TrackingRepo => throw new NotImplementedException();

		IHopArrivalRepository IDatabase.HopArrivalRepo => throw new NotImplementedException();

		public Mock.MockParcelRepository _parcelRepo;
        public Mock.MockRecipientRepository _recRepo;
        public Mock.MockTrackingInformationRepository _trackingRepo;
        public Mock.MockTruckRepository _truckRepo;
        public Mock.MockWarehouseRepository _warehouseRepo;
		public Mock.MockHopArrivalRepository _hopArrivalRepo;
    }
}
