using PLS.SKS.Package.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Interfaces
{
    public interface IParcelRepository
    {
        int Create(Parcel p); void
        Update(Parcel p); void
        Delete(int id);
        Parcel GetById(int id);
		Parcel GetByTrackingNumber(string trackingNumber);
    }
}
