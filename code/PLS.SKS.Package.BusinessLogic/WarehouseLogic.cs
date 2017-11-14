using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic
{
    public class WarehouseLogic
    {
		private DataAccess.Interfaces.IWarehouseRepository warehouseRepo;

		public WarehouseLogic(IServiceProvider serviceProvider)
		{
			warehouseRepo = new DataAccess.Sql.SqlWarehouseRepository(serviceProvider.GetRequiredService<DataAccess.Sql.DBContext>());
		}

		public DataAccess.Entities.Warehouse ExportWarehouses()
		{
			//Should return root warehouse!
			return warehouseRepo.GetById(1);
		}

		public void ImportWarehouses(DataAccess.Entities.Warehouse warehouse)
		{
			int pk = warehouseRepo.Create(warehouse);
		}
	}
}
