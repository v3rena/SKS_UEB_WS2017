﻿using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package;
using PLS.SKS.Package.DataAccess.Sql;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.Results;
using PLS.SKS.Package.BusinessLogic.Interfaces;
using Microsoft.Extensions.Logging;

namespace PLS.SKS.Package.BusinessLogic
{
    public class BusinessLogicFacade : Interfaces.IBusinessLogicFacade
    {
		private ILogger<BusinessLogicFacade> logger;
		private IHopArrivalLogic hopArrivalLogic;
		private IParcelEntryLogic parcelEntryLogic;
		private ITrackingLogic trackingLogic;
		private IWarehouseLogic warehouseLogic;

		public AutoMapper.IMapper Mapper { get; set; }

		public BusinessLogicFacade(IHopArrivalLogic hopArrivalLogic, IParcelEntryLogic parcelEntryLogic, ITrackingLogic trackingLogic, IWarehouseLogic warehouseLogic, ILogger<BusinessLogicFacade> logger, AutoMapper.IMapper mapper)
		{
			this.hopArrivalLogic = hopArrivalLogic;
			this.parcelEntryLogic = parcelEntryLogic;
			this.trackingLogic = trackingLogic;
			this.warehouseLogic = warehouseLogic;
			this.logger = logger;
            Mapper = mapper;
		}

		public void ScanParcel(string trackingNumber, string code)
        {
			logger.LogInformation("Calling the ScanParcel action");
			hopArrivalLogic.ScanParcel(trackingNumber, code);
		}

		public string AddParcel(IO.Swagger.Models.Parcel parcel)
        {
			logger.LogInformation("Calling the AddParcel action");
			Entities.Parcel blParcel = Mapper.Map<Entities.Parcel>(parcel);
			logger.LogError(ValidateParcel(blParcel));
			DataAccess.Entities.Parcel dalParcel = Mapper.Map<DataAccess.Entities.Parcel>(blParcel);
			return parcelEntryLogic.AddParcel(dalParcel);
        }

        public IO.Swagger.Models.TrackingInformation TrackParcel(string trackingNumber)
        {
			logger.LogInformation("Calling the TrackParcel action");
			DataAccess.Entities.Parcel dalParcel = trackingLogic.TrackParcel(trackingNumber);
			Entities.Parcel blParcel = Mapper.Map<Entities.Parcel>(dalParcel);
			logger.LogError(ValidateParcel(blParcel));
			IO.Swagger.Models.TrackingInformation info = Mapper.Map<IO.Swagger.Models.TrackingInformation>(blParcel.TrackingInformation);
			return info;
		}

		public IO.Swagger.Models.Warehouse ExportWarehouses()
		{
			logger.LogInformation("Calling the ExportWarehouses action");
			DataAccess.Entities.Warehouse dalWarehouse = warehouseLogic.ExportWarehouses();
			Entities.Warehouse blWarehouse = Mapper.Map<Entities.Warehouse>(dalWarehouse);
			logger.LogError(ValidateWarehouse(blWarehouse));
			IO.Swagger.Models.Warehouse serviceWarehouse = Mapper.Map<IO.Swagger.Models.Warehouse>(blWarehouse);
			return serviceWarehouse;
		}

		public void ImportWarehouses(IO.Swagger.Models.Warehouse warehouse)
		{
			logger.LogInformation("Calling the ImportWarehouses action");
			Entities.Warehouse blWarehouse = Mapper.Map<Entities.Warehouse>(warehouse);
			logger.LogError(ValidateWarehouse(blWarehouse));
			DataAccess.Entities.Warehouse dalWarehouse = Mapper.Map<DataAccess.Entities.Warehouse>(blWarehouse);
			warehouseLogic.ImportWarehouses(dalWarehouse);
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

		private string ValidateParcel(Entities.Parcel blParcel)
		{
			StringBuilder validationResults = new StringBuilder();

			Validator.ParcelValidator validator = new Validator.ParcelValidator();
			ValidationResult results = validator.Validate(blParcel);
			bool validationSucceeded = results.IsValid;
			IList<ValidationFailure> failures = results.Errors;

			foreach (var failure in failures)
			{
				validationResults.Append(failure);
			}
			return validationResults.ToString();
		}

		public void CreateMaps()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                //Swagger --> BL
                cfg.CreateMap<IO.Swagger.Models.Recipient, Entities.Recipient>();
                cfg.CreateMap<IO.Swagger.Models.Parcel, Entities.Parcel>()
                    .ForMember(model => model.TrackingInformation, option => option.Ignore())
                    .ForMember(model => model.TrackingNumber, option => option.Ignore());
                cfg.CreateMap<IO.Swagger.Models.Warehouse, Entities.Warehouse>();
				cfg.CreateMap<IO.Swagger.Models.Truck, Entities.Truck>(); //.ForMember(model => model.test, option => option.Ignore());
				cfg.CreateMap<IO.Swagger.Models.TrackingInformation, Entities.TrackingInformation>();
                cfg.CreateMap<IO.Swagger.Models.HopArrival, Entities.HopArrival>();
                //BL --> Swagger
                cfg.CreateMap<Entities.Parcel, IO.Swagger.Models.Parcel>();
                cfg.CreateMap<Entities.Recipient, IO.Swagger.Models.Recipient>();
                cfg.CreateMap<Entities.Warehouse, IO.Swagger.Models.Warehouse>();
                cfg.CreateMap<Entities.Truck, IO.Swagger.Models.Truck>();
                cfg.CreateMap<Entities.TrackingInformation, IO.Swagger.Models.TrackingInformation>();
                cfg.CreateMap<Entities.HopArrival, IO.Swagger.Models.HopArrival>();
                //DAL --> BL
                cfg.CreateMap<DataAccess.Entities.Recipient, Entities.Recipient>();
                cfg.CreateMap<DataAccess.Entities.Parcel, Entities.Parcel>();
                cfg.CreateMap<DataAccess.Entities.Warehouse, Entities.Warehouse>();
                cfg.CreateMap<DataAccess.Entities.Truck, Entities.Truck>();
                cfg.CreateMap<DataAccess.Entities.TrackingInformation, Entities.TrackingInformation>();
                cfg.CreateMap<DataAccess.Entities.HopArrival, Entities.HopArrival>();
                //BL --> DAL
                cfg.CreateMap<Entities.Recipient, DataAccess.Entities.Recipient>()
                    .ForMember(model => model.Id, option => option.Ignore());
                cfg.CreateMap<Entities.Parcel, DataAccess.Entities.Parcel>()
                    .ForMember(model => model.Id, option => option.Ignore())
                    .ForMember(model => model.TrackingInformationId, option => option.Ignore())
                    .ForMember(model => model.RecipientId, option => option.Ignore());
                cfg.CreateMap<Entities.Warehouse, DataAccess.Entities.Warehouse>()
                    .ForMember(model => model.Id, option => option.Ignore());
                cfg.CreateMap<Entities.Truck, DataAccess.Entities.Truck>()
                    .ForMember(model => model.Id, option => option.Ignore());

                cfg.CreateMap<Entities.TrackingInformation, DataAccess.Entities.TrackingInformation>()
                    .ForMember(model => model.Id, option => option.Ignore())
                    .AfterMap((s,d) => d.visitedHops.ForEach(
                        delegate(DataAccess.Entities.HopArrival h)
                        {
                            h.Status = "visited";
                        })
                    )
                    .AfterMap((s,d) => d.futureHops.ForEach(
                        delegate (DataAccess.Entities.HopArrival h)
                        {
                            h.Status = "future";
                        })
                    );

                cfg.CreateMap<Entities.HopArrival, DataAccess.Entities.HopArrival>()
                    .ForMember(model => model.Id, option => option.Ignore())
                    .ForMember(model => model.Status, option => option.Ignore())
                    .ForMember(model => model.TrackingInformation, option => option.Ignore())
                    .ForMember(model => model.TrackingInformationId, option => option.Ignore());
                ;
            }
            );

            config.AssertConfigurationIsValid();
            Mapper = config.CreateMapper();
        }
    }
}
