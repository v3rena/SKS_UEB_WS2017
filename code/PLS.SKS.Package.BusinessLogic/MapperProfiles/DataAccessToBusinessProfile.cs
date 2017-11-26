using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace PLS.SKS.Package.BusinessLogic.MapperProfiles
{
    public class DataAccessToBusinessProfile : Profile
    {
        public DataAccessToBusinessProfile()
        {
            CreateMap<DataAccess.Entities.Recipient, Entities.Recipient>();
            CreateMap<DataAccess.Entities.Parcel, Entities.Parcel>();
            CreateMap<DataAccess.Entities.Warehouse, Entities.Warehouse>();
            CreateMap<DataAccess.Entities.Truck, Entities.Truck>();
            CreateMap<DataAccess.Entities.TrackingInformation, Entities.TrackingInformation>();
            CreateMap<DataAccess.Entities.HopArrival, Entities.HopArrival>();
        }

        public static MapperConfiguration Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ServiceToBusinessProfile>();
            }
            );
            return config;
        }
    }
}
