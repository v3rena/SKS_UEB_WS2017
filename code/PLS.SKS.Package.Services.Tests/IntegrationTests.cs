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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

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
            // Act
            var response = await _client.GetAsync("/api/warehouse");
            response.EnsureSuccessStatusCode();


            var statuscode = response.StatusCode;
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual("OK", statuscode.ToString());
            Assert.IsNotNull(responseString);
		}

		[TestMethod]
		public async Task PostWarehouse()
		{
            //Assert
            var warehouse = new Warehouse("WH01", "Test", 12, new List<Warehouse> { new Warehouse("WH02", "Test", 12, new List<Warehouse>(), new List<Truck>()) }, new List<Truck>());
            var stringContent = new StringContent(JsonConvert.SerializeObject(warehouse), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/warehouse", stringContent);

            var responseStatus = response.StatusCode;

            // Assert
            Assert.AreEqual("OK", responseStatus.ToString());
        }

		[TestMethod]
		public async Task PostParcel()
		{
            //Assert
            var parcel = new Parcel(12, new Recipient("Tobias", "Test", "Testgasse 7", "A-1160", "Wien"));
            var stringContent = new StringContent(JsonConvert.SerializeObject(parcel), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/parcel", stringContent);

            var responseStatus = response.StatusCode;
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            StringAssert.Contains(responseString, "trackingId");
            Assert.AreEqual("OK", responseStatus.ToString());
        }

		[TestMethod]
		public async Task GetParcel()
		{
            // Act
            var response = await _client.GetAsync("/api/parcel/TN000001");
            response.EnsureSuccessStatusCode();

            var responseStatus = response.StatusCode;
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            StringAssert.Contains(responseString, "state");
            StringAssert.Contains(responseString, "visitedHops");
            StringAssert.Contains(responseString, "futureHops");
            Assert.AreEqual("OK", responseStatus.ToString());
        }

		[TestMethod]
		public async Task PostReportHop()
		{
            //Assert
            var stringContent = new StringContent("");

            // Act
            var response = await _client.PostAsync("/api/parcel/TN000001/reportHop/WH03", stringContent);

            var responseStatus = response.StatusCode;

            // Assert
            Assert.AreEqual("OK", responseStatus.ToString());
        }
	}
}
