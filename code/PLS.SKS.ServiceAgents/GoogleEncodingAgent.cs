using PLS.SKS.ServiceAgents.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using PLS.SKS.ServiceAgents.DTOs;

namespace PLS.SKS.ServiceAgents
{
	public class GoogleEncodingAgent : IGeoEncodingAgent
	{
		private static readonly string apiKey = "AIzaSyBx774ImXlKrTp_3Zr2ugSPgzD_Yjjk1QQ";
		private ILogger<GoogleEncodingAgent> logger;
		public AutoMapper.IMapper Mapper { get; set; }

		public GoogleEncodingAgent(ILogger<GoogleEncodingAgent> logger, AutoMapper.IMapper mapper)
		{
			this.logger = logger;
			this.Mapper = mapper;
		}

		public Location EncodeAddress(Recipient a)
		{
			var root = new GeoEncodingRoot();
			HttpClient client = GetClient();
			HttpResponseMessage response = client.GetAsync($"/maps/api/geocode/json?address={a}&key={apiKey}").Result;
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

				throw new
			}
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
	}
}
