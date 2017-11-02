using PLS.SKS.Package.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Interfaces
{
    public interface ITrackingInformationRepository
    {
        int Create(TrackingInformation t); void
        Update(TrackingInformation t); void
        Delete(int id);
        TrackingInformation GetById(int id);
    }
}
