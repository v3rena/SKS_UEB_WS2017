using Microsoft.Extensions.DependencyInjection;
using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic
{
    public class WarehouseLogic : Interfaces.IWarehouseLogic
	{
		private DataAccess.Interfaces.IWarehouseRepository warehouseRepo;

		public WarehouseLogic(IWarehouseRepository warehouseRepository)
		{
			warehouseRepo = warehouseRepository;
		}

		public DataAccess.Entities.Warehouse ExportWarehouses()
		{
			//Should return root warehouse
			return warehouseRepo.GetById(1);
		}

		public void ImportWarehouses(DataAccess.Entities.Warehouse warehouse)
		{
			warehouseRepo.Create(warehouse);
		}
	}
}
