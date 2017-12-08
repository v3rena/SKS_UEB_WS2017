using PLS.SKS.Package.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Interfaces
{
    public interface IWarehouseRepository
    {
        int Create(Warehouse w); void
        Update(Warehouse w); void
        Delete(int id);
        Warehouse GetById(int id);
		Warehouse GetParent(Truck truck);
		Warehouse GetParent(Warehouse warehouse);
		Warehouse GetRootWarehouse();
	}
}
