﻿using PLS.SKS.ServiceAgents.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using PLS.SKS.ServiceAgents.DTOs;
using Microsoft.Extensions.Logging;
using PLS.SKS.ServiceAgents.Helpers;

namespace PLS.SKS.ServiceAgents
{
	public class GoogleEncodingAgent : IGeoEncodingAgent
	{
		private static readonly string ApiKey = "AIzaSyBx774ImXlKrTp_3Zr2ugSPgzD_Yjjk1QQ";
		private readonly ILogger<GoogleEncodingAgent> _logger;

		public GoogleEncodingAgent(ILogger<GoogleEncodingAgent> logger)
		{
			_logger = logger;
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
			try
			{
				var root = new GeoEncodingRoot();
				string address = GetAddress(recipient);
				HttpClient client = GetClient();
				HttpResponseMessage response = client.GetAsync($"/maps/api/geocode/json?address={address}&key={ApiKey}").Result;
				if (response.IsSuccessStatusCode)
				{
					root = response.Content.ReadAsAsync<GeoEncodingRoot>().Result;
				}
				return root.Results.FirstOrDefault()?.Geometry.Location;
			}
			catch (Exception ex)
			{
				_logger.LogError("Failed trying to encode the given address", ex);
				throw new SaException("Failed trying to encode the given address", ex);
			}
		}

		private string GetAddress(Recipient recipient)
		{
			return recipient.Street + " " + recipient.PostalCode + " " + recipient.City;
		}
	}
}
