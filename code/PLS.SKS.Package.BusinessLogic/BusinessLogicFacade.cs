using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package;
using PLS.SKS.Package.DataAccess.Sql;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.Results;
using PLS.SKS.Package.BusinessLogic.Interfaces;

namespace PLS.SKS.Package.BusinessLogic
{
    public class BusinessLogicFacade : Interfaces.IBusinessLogicFacade
    {
        /*public BusinessLogicFacade(IServiceProvider serviceProvider)
        {
            hopArrivalLogic = new HopArrivalLogic(serviceProvider);
            parcelEntryLogic = new ParcelEntryLogic(serviceProvider);
            trackingLogic = new TrackingLogic(serviceProvider);
			warehouseLogic = new WarehouseLogic(serviceProvider);
            CreateMaps();
        }*/

		public BusinessLogicFacade(IHopArrivalLogic hopArrivalLogic, IParcelEntryLogic parcelEntryLogic, ITrackingLogic trackingLogic, IWarehouseLogic warehouseLogic, AutoMapper.IMapper mapper)
		{
			this.hopArrivalLogic = hopArrivalLogic;
			this.parcelEntryLogic = parcelEntryLogic;
			this.trackingLogic = trackingLogic;
			this.warehouseLogic = warehouseLogic;
            //CreateMaps();
            this.Mapper = mapper;
		}

		public void ScanParcel(string trackingNumber, string code)
        {
			Validator.ParcelValidator validator = new Validator.ParcelValidator();
			//ValidationResult results = validator.Validate(parcel);
			//bool validationSucceeded = results.IsValid;
			//IList<ValidationFailure> failures = results.Errors;

			hopArrivalLogic.ScanParcel(trackingNumber, code);
		}

		public string AddParcel(IO.Swagger.Models.Parcel parcel)
        {
			Entities.Parcel blParcel = Mapper.Map<Entities.Parcel>(parcel);

			//Validates BusinessLogicFacade model
			Validator.ParcelValidator validator = new Validator.ParcelValidator();
			ValidationResult results = validator.Validate(blParcel);
			bool validationSucceeded = results.IsValid;
			IList<ValidationFailure> failures = results.Errors;

			DataAccess.Entities.Parcel dalParcel = Mapper.Map<DataAccess.Entities.Parcel>(blParcel);
			string trNr = parcelEntryLogic.AddParcel(dalParcel);
			return trNr;
        }

        public IO.Swagger.Models.TrackingInformation TrackParcel(string trackingNumber)
        {
			DataAccess.Entities.Parcel dalParcel = trackingLogic.TrackParcel(trackingNumber);
			Entities.Parcel blParcel = Mapper.Map<Entities.Parcel>(dalParcel);
			//Implement exception handling

			//Validates BusinessLogicFacade model
			Validator.ParcelValidator validator = new Validator.ParcelValidator();
			ValidationResult results = validator.Validate(blParcel);
			bool validationSucceeded = results.IsValid;
			IList<ValidationFailure> failures = results.Errors;

			var trInfo = blParcel.TrackingInformation;
			IO.Swagger.Models.TrackingInformation info = Mapper.Map<IO.Swagger.Models.TrackingInformation>(trInfo);
			return info;
		}

		public IO.Swagger.Models.Warehouse ExportWarehouses()
		{
			//Should return root warehouse!
			DataAccess.Entities.Warehouse dalWarehouse = warehouseLogic.ExportWarehouses();
			Entities.Warehouse blWarehouse = Mapper.Map<Entities.Warehouse>(dalWarehouse);

			//Validates BusinessLogicFacade model
			Validator.WarehouseValidator validator = new Validator.WarehouseValidator();
			ValidationResult results = validator.Validate(blWarehouse);
			bool validationSucceeded = results.IsValid;
			IList<ValidationFailure> failures = results.Errors;

			IO.Swagger.Models.Warehouse swaggerWarehouse = Mapper.Map<IO.Swagger.Models.Warehouse>(blWarehouse);
			return swaggerWarehouse;
		}

		public void ImportWarehouses(IO.Swagger.Models.Warehouse warehouse)
		{
			Entities.Warehouse blWarehouse = Mapper.Map<Entities.Warehouse>(warehouse);

			//Validates BusinessLogicFacade model
			Validator.WarehouseValidator validator = new Validator.WarehouseValidator();
			ValidationResult results = validator.Validate(blWarehouse);
			bool validationSucceeded = results.IsValid;
			IList<ValidationFailure> failures = results.Errors;

			DataAccess.Entities.Warehouse dalWarehouse = Mapper.Map<DataAccess.Entities.Warehouse>(blWarehouse);
			warehouseLogic.ImportWarehouses(dalWarehouse);
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

        public AutoMapper.IMapper Mapper { get; set; }

        private Interfaces.IHopArrivalLogic hopArrivalLogic;
        private Interfaces.IParcelEntryLogic parcelEntryLogic;
        private Interfaces.ITrackingLogic trackingLogic;
		private Interfaces.IWarehouseLogic warehouseLogic;
    }
}
