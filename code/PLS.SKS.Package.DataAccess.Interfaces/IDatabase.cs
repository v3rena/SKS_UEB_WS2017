using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Interfaces
{
    public interface IDatabase
    {
        IHopArrivalRepository hopArrivalRepo { get; }
        IParcelRepository parcelRepo { get; }
        IRecipientRepository recipientRepo { get; }
        ITrackingInformationRepository trackingRepo { get; }
        ITruckRepository truckRepo { get; }
        IWarehouseRepository warehouseRepo { get; }
    }
}
