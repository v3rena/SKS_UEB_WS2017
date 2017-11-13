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

        public void addParcel(IO.Swagger.Models.Parcel parcel)
        {
			Entities.Parcel blParcel = Mapper.Map<Entities.Parcel>(parcel);

			//Validates BusinessLogic model
			Validator.ParcelValidator validator = new Validator.ParcelValidator();
			ValidationResult results = validator.Validate(blParcel);
			bool validationSucceeded = results.IsValid;
			IList<ValidationFailure> failures = results.Errors;

			DataAccess.Entities.Parcel dalParcel = Mapper.Map<DataAccess.Entities.Parcel>(blParcel);
			parcelEntryLogic.addParcel(dalParcel);
        }

        public IO.Swagger.Models.Parcel trackParcel(string trackingNumber)
        {
			DataAccess.Entities.Parcel dalParcel = trackingLogic.trackParcel(trackingNumber);

			Entities.Parcel blParcel = Mapper.Map<Entities.Parcel>(dalParcel);
			//IO.Swagger.Models.Parcel sParcel = Mapper.Map<IO.Swagger.Models.Parcel>(blParcel);
			//Testweise Erstellung eines Swagger-Parcels, weil Mapping (noch) nicht funktioniert
			IO.Swagger.Models.Parcel sParcel = new IO.Swagger.Models.Parcel(22, new IO.Swagger.Models.Recipient("Hans Peter", "Doskozil", "Industriestrasse 39", "1210", "Wien"));

			return sParcel;
		}

        public void Test()
        {
            Entities.Recipient blRec = new Entities.Recipient("fName", "lName", "testStreet", "1234", "testCity");
            Entities.Parcel blParcel = new Entities.Parcel(2.0f, blRec);

            IO.Swagger.Models.Parcel swParcel = Mapper.Map<IO.Swagger.Models.Parcel>(blParcel);

            return;
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

                cfg.CreateMap<Entities.Parcel, IO.Swagger.Models.Parcel>();
                cfg.CreateMap<Entities.Recipient, IO.Swagger.Models.Recipient>();

            }
            );

            config.AssertConfigurationIsValid();
            Mapper = config.CreateMapper();
        }

        public AutoMapper.IMapper Mapper { get; set; }

        private HopArrivalLogic hopArrivalLogic;
        private ParcelEntryLogic parcelEntryLogic;
        private TrackingLogic trackingLogic;
    }
}
