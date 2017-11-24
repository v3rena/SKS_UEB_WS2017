using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic.MapperProfiles
{
    public class ServiceToBusinessProfile : Profile
    {
        public ServiceToBusinessProfile()
        {
            CreateMap<IO.Swagger.Models.Recipient, Entities.Recipient>();
            CreateMap<IO.Swagger.Models.Parcel, Entities.Parcel>()
                .ForMember(model => model.TrackingInformation, option => option.Ignore())
                .ForMember(model => model.TrackingNumber, option => option.Ignore());
            CreateMap<IO.Swagger.Models.Warehouse, Entities.Warehouse>();
            CreateMap<IO.Swagger.Models.Truck, Entities.Truck>(); 
            CreateMap<IO.Swagger.Models.TrackingInformation, Entities.TrackingInformation>();
            CreateMap<IO.Swagger.Models.HopArrival, Entities.HopArrival>();
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
