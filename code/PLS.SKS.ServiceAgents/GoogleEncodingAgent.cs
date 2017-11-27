using PLS.SKS.ServiceAgents.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PLS.SKS.ServiceAgents.DTOs;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PLS.SKS.ServiceAgents
{
	public class GoogleEncodingAgent : IGeoEncodingAgent
	{
		private static readonly string apiKey = "de01ffd9808244fcbbfa65d675ee6fd0";

		public GeoCoordinates EncodeAddress(Address a)
		{
			GeoCoordinates coordinates = new GeoCoordinates();
			HttpClient client = GetClient();
			HttpResponseMessage response = client.GetAsync($"path-to-google-api").Result;
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
				BaseAddress = new Uri("https://newsapi.org")
			};
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			return client;
		}
	}
}
