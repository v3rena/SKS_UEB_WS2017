using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace PLS.SKS.Package.BusinessLogic.MapperProfiles
{
    public class BusinessToDataAccessProfile : Profile
    {
        public BusinessToDataAccessProfile()
        {
            CreateMap<Entities.Recipient, DataAccess.Entities.Recipient>()
                .ForMember(model => model.Id, option => option.Ignore());
			CreateMap<Entities.HopArrival, DataAccess.Entities.HopArrival>()
			   .ForMember(model => model.Id, option => option.Ignore())
			   .ForMember(model => model.TrackingInformationId, option => option.Ignore());
			CreateMap<Entities.Parcel, DataAccess.Entities.Parcel>()
                .ForMember(model => model.Id, option => option.Ignore())
                .ForMember(model => model.TrackingInformationId, option => option.Ignore())
                .ForMember(model => model.RecipientId, option => option.Ignore());
            CreateMap<Entities.Warehouse, DataAccess.Entities.Warehouse>()
                .ForMember(model => model.Id, option => option.Ignore());
            CreateMap<Entities.Truck, DataAccess.Entities.Truck>()
                .ForMember(model => model.Id, option => option.Ignore());
            CreateMap<Entities.TrackingInformation, DataAccess.Entities.TrackingInformation>()
                .ForMember(model => model.Id, option => option.Ignore())
                .AfterMap((s, d) => d.visitedHops.ForEach(
                    delegate (DataAccess.Entities.HopArrival h)
                    {
                        h.Status = "visited";
                    })
                )
                .AfterMap((s, d) => d.futureHops.ForEach(
                    delegate (DataAccess.Entities.HopArrival h)
                    {
                        h.Status = "future";
                    })
                );
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
