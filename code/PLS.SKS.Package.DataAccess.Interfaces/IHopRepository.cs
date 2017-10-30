using PLS.SKS.Package.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Interfaces
{
    public interface IHopRepository
    {
        int Create(Hop h); void
        Update(Hop h); void
        Delete(int id);
        Hop GetById(int id);
    }
}
