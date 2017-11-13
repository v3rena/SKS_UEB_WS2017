using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package;
using PLS.SKS.Package.DataAccess.Sql;
using Microsoft.Extensions.DependencyInjection;

namespace PLS.SKS.Package.BusinessLogic
{
    public class BusinessLogic
    {
        public BusinessLogic(IServiceProvider serviceProvider)
        {
			parcelRepo = new DataAccess.Sql.SqlParcelRepository(serviceProvider.GetRequiredService<DBContext>());
            hopArrivalLogic = new HopArrivalLogic();
            //parcelEntryLogic = new ParcelEntryLogic(serviceProvider.GetRequiredService<DBContext>());
            trackingLogic = new TrackingLogic();
            createMaps();
        }

		/*public BusinessLogic(DataAccess.Sql.SqlParcelRepository sqlParcelRepository)
		{
			parcelRepo = sqlParcelRepository;
			hopArrivalLogic = new HopArrivalLogic();
			parcelEntryLogic = new ParcelEntryLogic();
			trackingLogic = new TrackingLogic();
			createMaps();
		}*/

		public void scanParcel()
        {
            //hopArrivalLogic.scanParcel(new Entities.Parcel(), "test");
        }

        public void addParcel(IO.Swagger.Models.Parcel parcel)
        {
			//Entities.Parcel blParcel = Mapper.Map<Entities.Parcel>(parcel);
			//DataAccess.Entities.Parcel dalParcel = Mapper.Map<DataAccess.Entities.Parcel>(blParcel);
			//parcelEntryLogic.addParcel(dalParcel);
        }

        public IO.Swagger.Models.Parcel trackParcel(string trackingNumber)
        {
			int nr = Convert.ToInt32(trackingNumber);
			DataAccess.Entities.Parcel dalParcel = parcelRepo.GetById(nr);

			Entities.Parcel blParcel = Mapper.Map<Entities.Parcel>(dalParcel);
			IO.Swagger.Models.Parcel sParcel = Mapper.Map<IO.Swagger.Models.Parcel>(blParcel);

			return sParcel;
		}

		private void createMaps()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IO.Swagger.Models.Recipient, Entities.Recipient>();
                cfg.CreateMap<IO.Swagger.Models.Parcel, Entities.Parcel>().ForMember(model => model.trackingInformation, option => option.Ignore()).ForMember(model => model.trackingNumber, option => option.Ignore());
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
		private DataAccess.Interfaces.IParcelRepository parcelRepo;
    }
}
