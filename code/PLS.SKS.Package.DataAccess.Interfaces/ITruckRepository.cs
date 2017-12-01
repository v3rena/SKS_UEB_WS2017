using PLS.SKS.Package.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Interfaces
{
    public interface ITruckRepository
    {
        int Create(Truck t); void
        Update(Truck t); void
        Delete(int id);
        Truck GetById(int id);
		List<Truck> GetAll();
    }
}
