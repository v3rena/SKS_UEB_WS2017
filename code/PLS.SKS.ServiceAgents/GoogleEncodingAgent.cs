using PLS.SKS.ServiceAgents.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using PLS.SKS.ServiceAgents.DTOs;
using Microsoft.Extensions.Logging;

namespace PLS.SKS.ServiceAgents
{
	public class GoogleEncodingAgent : IGeoEncodingAgent
	{
		private static readonly string apiKey = "AIzaSyBx774ImXlKrTp_3Zr2ugSPgzD_Yjjk1QQ";
		private ILogger<GoogleEncodingAgent> logger;
		private AutoMapper.IMapper mapper;

		public GoogleEncodingAgent(ILogger<GoogleEncodingAgent> logger, AutoMapper.IMapper mapper)
		{
			this.logger = logger;
			this.mapper = mapper;
		}

		private HttpClient GetClient()
		{
			HttpClient client = new HttpClient()
			{
				BaseAddress = new Uri("https://maps.googleapis.com")
			};
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			return client;
		}

		public Location EncodeAddress(Recipient recipient)
		{
			var root = new GeoEncodingRoot();
			string address = GetAddress(recipient);
			HttpClient client = GetClient();
			HttpResponseMessage response = client.GetAsync($"/maps/api/geocode/json?address={address}&key={apiKey}").Result;
			if (response.IsSuccessStatusCode)
			{
				root = response.Content.ReadAsAsync<GeoEncodingRoot>().Result;
			}
			try
			{
				return root.Results.FirstOrDefault().Geometry.Location;
			}
			catch(Exception ex)
			{
				logger.LogError(ex.ToString());
				throw new Exception();
			}
		}

		private string GetAddress(Recipient recipient)
		{
			return recipient.Street + " " + recipient.PostalCode + " " + recipient.City;
		}
	}
}
