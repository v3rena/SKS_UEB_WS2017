using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic.MapperProfiles
{
	public class ServiceAgentsToBusinnesProfile : Profile
	{
		public ServiceAgentsToBusinnesProfile()
		{
			CreateMap<ServiceAgents.DTOs.Location, Entities.Location>();
			CreateMap<Entities.Recipient, ServiceAgents.DTOs.Recipient>().ForMember(model => model.FirstName, option => option.Ignore())
				.ForMember(model => model.LastName, option => option.Ignore());
		}

		public static MapperConfiguration Initialize()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<ServiceAgentsToBusinnesProfile>();
			}
			);
			return config;
		}
	}
}
