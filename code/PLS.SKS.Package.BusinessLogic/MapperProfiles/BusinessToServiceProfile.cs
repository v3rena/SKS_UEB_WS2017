using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace PLS.SKS.Package.BusinessLogic.MapperProfiles
{
    public class BusinessToServiceProfile : Profile
    {
        public BusinessToServiceProfile()
        {
            CreateMap<Entities.Parcel, IO.Swagger.Models.Parcel>();
            CreateMap<Entities.Recipient, IO.Swagger.Models.Recipient>();
            CreateMap<Entities.Warehouse, IO.Swagger.Models.Warehouse>();
            CreateMap<Entities.Truck, IO.Swagger.Models.Truck>();
            CreateMap<Entities.TrackingInformation, IO.Swagger.Models.TrackingInformation>();
            CreateMap<Entities.HopArrival, IO.Swagger.Models.HopArrival>();
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
