using FluentValidation.Results;
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
		private IDbCleaner dbCleaner;
		private AutoMapper.IMapper mapper;

		public WarehouseLogic(IWarehouseRepository warehouseRepository, ILogger<WarehouseLogic> logger, AutoMapper.IMapper mapper, IDbCleaner dbCleaner)
		{
			warehouseRepo = warehouseRepository;
			this.logger = logger;
			this.mapper = mapper;
			this.dbCleaner = dbCleaner;
		}

		public IO.Swagger.Models.Warehouse ExportWarehouses()
		{
			try
			{
				var dalWarehouse = warehouseRepo.GetRootWarehouse();
				if (dalWarehouse == null)
				{
					throw new BLException("No RootWarehouse found");
				}
				Entities.Warehouse blWarehouse = mapper.Map<Entities.Warehouse>(dalWarehouse);
				if (blWarehouse != null)
				{
					string validationResults = ValidateWarehouse(blWarehouse);
					if (validationResults != "")
					{
						logger.LogError(validationResults);
						throw new BLException("Given Warehouse is not valid");
					}
				}
				IO.Swagger.Models.Warehouse serviceWarehouse = mapper.Map<IO.Swagger.Models.Warehouse>(blWarehouse);
				return serviceWarehouse;
			}
			catch(Exception ex)
			{
				logger.LogError("Cannot export warehouses", ex);
				throw new BLException("Cannot export warehouses", ex);
			}
		}

		public void ImportWarehouses(IO.Swagger.Models.Warehouse warehouse)
		{
			try
			{
				Entities.Warehouse blWarehouse = mapper.Map<Entities.Warehouse>(warehouse);
				if (blWarehouse != null)
				{
					string validationResults = ValidateWarehouse(blWarehouse);
					if (validationResults != "")
					{
						logger.LogError(validationResults);
						throw new BLException("Given Warehouse is not valid");
					}
				}
				DataAccess.Entities.Warehouse dalWarehouse = mapper.Map<DataAccess.Entities.Warehouse>(blWarehouse);
				dbCleaner.CleanDb();
				warehouseRepo.Create(dalWarehouse);
			}
			catch (Exception ex)
			{
				logger.LogError("Cannot import warehouses", ex);
				throw new BLException("Cannot import warehouses", ex);
			}
		}

		private string ValidateWarehouse(Entities.Warehouse blWarehouse)
		{
			StringBuilder validationResults = new StringBuilder();

			Validator.WarehouseValidator validator = new Validator.WarehouseValidator();
			ValidationResult results = validator.Validate(blWarehouse);
			bool validationSucceeded = results.IsValid;
			IList<ValidationFailure> failures = results.Errors;

			foreach (var failure in failures)
			{
				validationResults.Append(failure);
			}
			return validationResults.ToString();
		}
	}
}
