using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Interfaces
{
    public interface IDatabase
    {
        IHopArrivalRepository HopArrivalRepo { get; }
        IParcelRepository ParcelRepo { get; }
        IRecipientRepository RecipientRepo { get; }
        ITrackingInformationRepository TrackingRepo { get; }
        ITruckRepository TruckRepo { get; }
        IWarehouseRepository WarehouseRepo { get; }
    }
}
