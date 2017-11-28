using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic
{
    public class WarehouseLogic : Interfaces.IWarehouseLogic
	{
		private IWarehouseRepository warehouseRepo;
		private ILogger<WarehouseLogic> logger;
		public AutoMapper.IMapper mapper { get; set; }

		public WarehouseLogic(IWarehouseRepository warehouseRepository, ILogger<WarehouseLogic> logger, AutoMapper.IMapper mapper)
		{
			warehouseRepo = warehouseRepository;
			this.logger = logger;
			this.mapper = mapper;
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
