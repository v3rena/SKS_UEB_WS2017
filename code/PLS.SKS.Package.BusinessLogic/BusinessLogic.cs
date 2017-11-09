using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package;


namespace PLS.SKS.Package.BusinessLogic
{
    public class BusinessLogic
    {
        public BusinessLogic()
        {
			parcelRepo = new DataAccess.Sql.SqlParcelRepository();
            hopArrivalLogic = new HopArrivalLogic();
            parcelEntryLogic = new ParcelEntryLogic();
            trackingLogic = new TrackingLogic();
            createMaps();
        }

        public void scanParcel()
        {
            hopArrivalLogic.scanParcel(new Entities.Parcel(), "test");
        }

        public void addParcel(IO.Swagger.Models.Parcel parcel)
        {
			Entities.Parcel blParcel = Mapper.Map<Entities.Parcel>(parcel);
			DataAccess.Entities.Parcel dalParcel = Mapper.Map<DataAccess.Entities.Parcel>(blParcel);
			parcelEntryLogic.addParcel(dalParcel);
        }

        public void trackParcel()
        {
            trackingLogic.trackParcel("test");
        }

        private void createMaps()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IO.Swagger.Models.Recipient, Entities.Recipient>();
                cfg.CreateMap<IO.Swagger.Models.Parcel, Entities.Parcel>();
                cfg.CreateMap<IO.Swagger.Models.Warehouse, Entities.Warehouse>();
                cfg.CreateMap<IO.Swagger.Models.Truck, Entities.Truck>();
                cfg.CreateMap<IO.Swagger.Models.TrackingInformation, Entities.TrackingInformation>();
                cfg.CreateMap<IO.Swagger.Models.HopArrival, Entities.HopArrival>();

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
		private DataAccess.Interfaces.IParcelRepository parcelRepo;
    }
}
