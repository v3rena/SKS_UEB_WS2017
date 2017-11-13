using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package;
using PLS.SKS.Package.DataAccess.Sql;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.Results;

namespace PLS.SKS.Package.BusinessLogic
{
    public class BusinessLogic
    {
        public BusinessLogic(IServiceProvider serviceProvider)
        {
            hopArrivalLogic = new HopArrivalLogic(serviceProvider);
            parcelEntryLogic = new ParcelEntryLogic(serviceProvider);
            trackingLogic = new TrackingLogic(serviceProvider);
			warehouseLogic = new WarehouseLogic(serviceProvider);
            createMaps();
        }

		public void scanParcel()
        {
			Validator.ParcelValidator validator = new Validator.ParcelValidator();
			//ValidationResult results = validator.Validate(parcel);
			//bool validationSucceeded = results.IsValid;
			//IList<ValidationFailure> failures = results.Errors;

			//hopArrivalLogic.scanParcel()
        }

        public string addParcel(IO.Swagger.Models.Parcel parcel)
        {
			Entities.Parcel blParcel = Mapper.Map<Entities.Parcel>(parcel);

			//Validates BusinessLogic model
			Validator.ParcelValidator validator = new Validator.ParcelValidator();
			ValidationResult results = validator.Validate(blParcel);
			bool validationSucceeded = results.IsValid;
			IList<ValidationFailure> failures = results.Errors;

			DataAccess.Entities.Parcel dalParcel = Mapper.Map<DataAccess.Entities.Parcel>(blParcel);
			string trNr = parcelEntryLogic.addParcel(dalParcel);
			return trNr;
        }

        public IO.Swagger.Models.TrackingInformation trackParcel(string trackingNumber)
        {
			DataAccess.Entities.Parcel dalParcel = trackingLogic.trackParcel(trackingNumber);
			Entities.Parcel blParcel = Mapper.Map<Entities.Parcel>(dalParcel);
			var trInfo = blParcel.trackingInformation;
			IO.Swagger.Models.TrackingInformation info = Mapper.Map<IO.Swagger.Models.TrackingInformation>(trInfo);
			return info;
		}

		public IO.Swagger.Models.Warehouse ExportWarehouses()
		{
			//Should return root warehouse!
			DataAccess.Entities.Warehouse dalWarehouse = warehouseLogic.ExportWarehouses();
			Entities.Warehouse blWarehouse = Mapper.Map<Entities.Warehouse>(dalWarehouse);
			IO.Swagger.Models.Warehouse swaggerWarehouse = Mapper.Map<IO.Swagger.Models.Warehouse>(blWarehouse);
			return swaggerWarehouse;
		}

		public void ImportWarehouses(IO.Swagger.Models.Warehouse warehouse)
		{
			Entities.Warehouse blWarehouse = Mapper.Map<Entities.Warehouse>(warehouse);

			//Validates BusinessLogic model
			Validator.WarehouseValidator validator = new Validator.WarehouseValidator();
			ValidationResult results = validator.Validate(blWarehouse);
			bool validationSucceeded = results.IsValid;
			IList<ValidationFailure> failures = results.Errors;

			DataAccess.Entities.Warehouse dalWarehouse = Mapper.Map<DataAccess.Entities.Warehouse>(blWarehouse);
			warehouseLogic.ImportWarehouses(dalWarehouse);
		}

		private void createMaps()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IO.Swagger.Models.Recipient, Entities.Recipient>();
                cfg.CreateMap<IO.Swagger.Models.Parcel, Entities.Parcel>().ForMember(model => model.TrackingInformation, option => option.Ignore()).ForMember(model => model.TrackingNumber, option => option.Ignore());
                cfg.CreateMap<IO.Swagger.Models.Warehouse, Entities.Warehouse>();
				cfg.CreateMap<IO.Swagger.Models.Truck, Entities.Truck>(); //.ForMember(model => model.test, option => option.Ignore());
				cfg.CreateMap<IO.Swagger.Models.TrackingInformation, Entities.TrackingInformation>();
                cfg.CreateMap<IO.Swagger.Models.HopArrival, Entities.HopArrival>().ForMember(model => model.trackingInformation, option => option.Ignore());

                cfg.CreateMap<DataAccess.Entities.Recipient, Entities.Recipient>();
                cfg.CreateMap<DataAccess.Entities.Parcel, Entities.Parcel>();
                cfg.CreateMap<DataAccess.Entities.Warehouse, Entities.Warehouse>();
                cfg.CreateMap<DataAccess.Entities.Truck, Entities.Truck>();
                cfg.CreateMap<DataAccess.Entities.TrackingInformation, Entities.TrackingInformation>();
                cfg.CreateMap<DataAccess.Entities.HopArrival, Entities.HopArrival>();
            }
            );

            config.AssertConfigurationIsValid();
            Mapper = config.CreateMapper();
        }

        public AutoMapper.IMapper Mapper { get; set; }

        private HopArrivalLogic hopArrivalLogic;
        private ParcelEntryLogic parcelEntryLogic;
        private TrackingLogic trackingLogic;
		private WarehouseLogic warehouseLogic;
    }
}
