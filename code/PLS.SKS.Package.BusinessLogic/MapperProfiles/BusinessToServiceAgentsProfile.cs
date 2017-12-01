using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic.MapperProfiles
{
	public class BusinessToServiceAgentsProfile : Profile
	{
		public BusinessToServiceAgentsProfile()
		{
			CreateMap<Entities.Location, ServiceAgents.DTOs.Location>();
			CreateMap<Entities.Recipient, ServiceAgents.DTOs.Recipient>().ForMember(model => model.FirstName, option => option.Ignore())
				.ForMember(model => model.LastName, option => option.Ignore());
		}

		public static MapperConfiguration Initialize()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<BusinessToServiceAgentsProfile>();
			}
			);
			return config;
		}
	}
}
