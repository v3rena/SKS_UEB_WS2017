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

		public GeoCoordinates EncodeAddress(Address a)
		{
			GeoCoordinates coordinates = new GeoCoordinates();
			HttpClient client = GetClient();
			HttpResponseMessage response = client.GetAsync($"/maps/api/geocode/json?address={a}&key={apiKey}").Result;
			if (response.IsSuccessStatusCode)
			{
				coordinates = response.Content.ReadAsAsync<GeoCoordinates>().Result;
			}
			return coordinates;
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
