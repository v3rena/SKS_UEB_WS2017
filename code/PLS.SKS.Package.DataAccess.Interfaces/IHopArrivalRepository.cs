using PLS.SKS.Package.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Interfaces
{
    public interface IHopArrivalRepository
    {
        int Create(HopArrival h); void
        Update(HopArrival h); void
        Delete(int id);
        HopArrival GetById(int id);
        List<HopArrival> GetByTrackingInforamtionId(int id);
    }
}
