using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.BusinessLogic.Helpers;
using PLS.SKS.Package.BusinessLogic.Validators;

namespace PLS.SKS.Package.BusinessLogic
{
    public class WarehouseLogic : Interfaces.IWarehouseLogic
	{
		private readonly IWarehouseRepository _warehouseRepo;
		private readonly ILogger<WarehouseLogic> _logger;
		private readonly IDbCleaner _dbCleaner;
		private readonly AutoMapper.IMapper _mapper;

		public WarehouseLogic(IWarehouseRepository warehouseRepository, ILogger<WarehouseLogic> logger, AutoMapper.IMapper mapper, IDbCleaner dbCleaner)
		{
			_warehouseRepo = warehouseRepository;
			_logger = logger;
			_mapper = mapper;
			_dbCleaner = dbCleaner;
		}

		public IO.Swagger.Models.Warehouse ExportWarehouses()
		{
			try
			{
				var dalWarehouse = _warehouseRepo.GetRootWarehouse();
				if (dalWarehouse == null)
				{
					throw new BlException("No RootWarehouse found");
				}
				var blWarehouse = _mapper.Map<Entities.Warehouse>(dalWarehouse);
				if (blWarehouse != null)
				{
					string validationResults = ValidateWarehouse(blWarehouse);
					if (validationResults != "")
					{
						_logger.LogError(validationResults);
						throw new BlException("Given Warehouse is not valid");
					}
				}
				var serviceWarehouse = _mapper.Map<IO.Swagger.Models.Warehouse>(blWarehouse);
				return serviceWarehouse;
			}
			catch(Exception ex)
			{
				_logger.LogError("Cannot export warehouses", ex);
				throw new BlException("Cannot export warehouses", ex);
			}
		}

		public void ImportWarehouses(IO.Swagger.Models.Warehouse warehouse)
		{
			try
			{
				var blWarehouse = _mapper.Map<Entities.Warehouse>(warehouse);
				if (blWarehouse != null)
				{
					string validationResults = ValidateWarehouse(blWarehouse);
					if (validationResults != "")
					{
						_logger.LogError(validationResults);
						throw new BlException("Given Warehouse is not valid");
					}
				}
				var dalWarehouse = _mapper.Map<DataAccess.Entities.Warehouse>(blWarehouse);
				_dbCleaner.CleanDb();
				_warehouseRepo.Create(dalWarehouse);
			}
			catch (Exception ex)
			{
				_logger.LogError("Cannot import warehouses", ex);
				throw new BlException("Cannot import warehouses", ex);
			}
		}

		private string ValidateWarehouse(Entities.Warehouse blWarehouse)
		{
			StringBuilder validationResults = new StringBuilder();
			var validator = new WarehouseValidator();
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
