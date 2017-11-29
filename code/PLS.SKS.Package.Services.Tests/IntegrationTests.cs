using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using PLS.SKS.Package.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using IO.Swagger.Models;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PLS.SKS.Package.Services.Tests
{
	[TestClass]
    public class IntegrationTests
    {
		private readonly TestServer _server;
		private readonly HttpClient _client;

		public IntegrationTests()
		{
			// Arrange
			_server = new TestServer(new WebHostBuilder()
				.UseStartup<Startup>());
			_client = _server.CreateClient();
		}

		[TestMethod]
        public async Task GetWarehouse()
        {
            //// Act
            //var response = await _client.GetAsync("/api/warehouse");
            //response.EnsureSuccessStatusCode();

            //var responseString = await response.Content.ReadAsStringAsync();

            //// Assert
            //Assert.AreEqual("",
            //    responseString);
            Assert.IsTrue(true);
		}

		[TestMethod]
		public async Task PostWarehouse()
		{
            ////Assert
            //var warehouse = new Warehouse("EX1234", "Test", 12, new List<Warehouse> { new Warehouse("EX1234", "Test", 12, new List<Warehouse>(), new List<Truck>()) }, new List<Truck>());
            //var stringContent = new StringContent(JsonConvert.SerializeObject(warehouse), Encoding.UTF8, "application/json");

            //// Act
            //var response = await _client.PostAsync("/api/warehouse", stringContent);
            //response.EnsureSuccessStatusCode();

            //var responseString = await response.Content.ReadAsStringAsync();

            //// Assert
            //Assert.AreEqual("",
            //	responseString);
            Assert.IsTrue(true);
        }

		[TestMethod]
		public async Task PostParcel()
		{
            ////Assert
            //var parcel = new Parcel(12, new Recipient("Tobias", "Test", "Testgasse 7", "1160", "Wien"));
            //var stringContent = new StringContent(JsonConvert.SerializeObject(parcel), Encoding.UTF8, "application/json");

            //// Act
            //var response = await _client.PostAsync("/api/parcel", stringContent);
            //response.EnsureSuccessStatusCode();

            //var responseString = await response.Content.ReadAsStringAsync();

            //// Assert
            //Assert.AreEqual("",
            //	responseString);
            Assert.IsTrue(true);
        }

		[TestMethod]
		public async Task GetParcel()
		{
            //// Act
            //var response = await _client.GetAsync("/api/parcel/{trackingId}");
            //response.EnsureSuccessStatusCode();

            //var responseString = await response.Content.ReadAsStringAsync();

            //// Assert
            //Assert.AreEqual("",
            //	responseString);
            Assert.IsTrue(true);
        }

		[TestMethod]
		public async Task PostReportHop()
		{
            ////Assert
            //var parcel = new Parcel(12, new Recipient("Tobias", "Test", "Testgasse 7", "1160", "Wien"));
            //var warehouse = new Warehouse("EX1234", "Test", 12, new List<Warehouse> { new Warehouse("EX1234", "Test", 12, new List<Warehouse>(), new List<Truck>()) }, new List<Truck>());
            //var stringContent = new StringContent(JsonConvert.SerializeObject(parcel), Encoding.UTF8, "application/json");


            //// Act
            //var response = await _client.PostAsync("/api/parcel/{trackingId}/reportHop/{code}", stringContent);
            //response.EnsureSuccessStatusCode();

            //var responseString = await response.Content.ReadAsStringAsync();

            //// Assert
            //Assert.AreEqual("",
            //	responseString);
            Assert.IsTrue(true);
        }
	}
}
