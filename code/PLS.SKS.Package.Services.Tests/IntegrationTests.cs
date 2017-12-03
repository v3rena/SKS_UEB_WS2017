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
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace PLS.SKS.Package.Services.Tests
{
	[TestClass]
    public class IntegrationTests
    {
		private readonly TestServer _server;
		private readonly HttpClient _client;
		private string _testTrackingNumber;

		public IntegrationTests()
		{
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                          .UseStartup<Startup>()
                          .ConfigureAppConfiguration((hostContext, config) =>
                          {
                              config.AddEnvironmentVariables();
                          })
                          .ConfigureServices(services =>
                          {
                              services.AddDbContext<DataAccess.Sql.DBContext>(options =>
                              {
                                  var connectionStringBuilder =
                                        new SqlConnectionStringBuilder("Server = (localdb)\\mssqllocaldb; Database = ParcelLogisticsDB; Trusted_Connection = True; MultipleActiveResultSets = true");
                                  var sqlConnection = new SqlConnection(connectionStringBuilder.ToString());
                                  sqlConnection.Open();
                                  options.UseSqlServer(sqlConnection);

                              });
                          }));
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
            //Arrange
            var warehouse = new Warehouse("WH01", "Test", 12, new List<Warehouse> { new Warehouse("WH02", "Test", 12, new List<Warehouse>(), new List<Truck>() { new Truck("TR01", "WR-2765", 48.2089816m, 16.373213299999975m, 30m, 1m) }) }, new List<Truck>());
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
            //Arrange
            var parcel = new Parcel(12, new Recipient("Tobias", "Test", "Horvathgasse 2", "A-1160", "Wien"));
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
			//Arrange
			var parcel = new Parcel(12, new Recipient("Tobias", "Test", "Horvathgasse 2", "A-1160", "Wien"));
			var stringContent = new StringContent(JsonConvert.SerializeObject(parcel), Encoding.UTF8, "application/json");
			var responseArr = await _client.PostAsync("/api/parcel", stringContent);
			var responseStatusArr = responseArr.StatusCode;
			var responseStringArr = await responseArr.Content.ReadAsStringAsync();
			_testTrackingNumber = GetTrackingNumber(responseStringArr);

			// Act
			var response = await _client.GetAsync($"/api/parcel/{_testTrackingNumber}");
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
            //Arrange
            var stringContent = new StringContent("");
			var parcel = new Parcel(12, new Recipient("Tobias", "Test", "Horvathgasse 2", "A-1160", "Wien"));
			var stringContentArr = new StringContent(JsonConvert.SerializeObject(parcel), Encoding.UTF8, "application/json");
			var responseArr = await _client.PostAsync("/api/parcel", stringContentArr);
			var responseStatusArr = responseArr.StatusCode;
			var responseStringArr = await responseArr.Content.ReadAsStringAsync();
			_testTrackingNumber = GetTrackingNumber(responseStringArr);

			// Act
			var response = await _client.PostAsync($"/api/parcel/{_testTrackingNumber}/reportHop/WH02", stringContent);

            var responseStatus = response.StatusCode;

            // Assert
            Assert.AreEqual("OK", responseStatus.ToString());
        }

		private string GetTrackingNumber(string input)
		{
			var inputs = input.Split("\"");
			var answer = inputs[3];
			answer = answer.Replace("\"", " ");
			answer = answer.Trim();
			return answer;
		}
	}
}
