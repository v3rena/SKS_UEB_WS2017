using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using PLS.SKS.Package.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;

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

			var responseString = await response.Content.ReadAsStringAsync();

			// Assert
			Assert.AreEqual("",
				responseString);
		}

		[TestMethod]
		public async Task PostWarehouse()
		{
			// Act
			var response = await _client.GetAsync("/api/warehouse");
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();

			// Assert
			Assert.AreEqual("",
				responseString);
		}

		[TestMethod]
		public async Task PostParcel()
		{
			// Act
			var response = await _client.GetAsync("/api/parcel");
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();

			// Assert
			Assert.AreEqual("",
				responseString);
		}

		[TestMethod]
		public async Task GetParcel()
		{
			// Act
			var response = await _client.GetAsync("/api/parcel/{trackingId}");
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();

			// Assert
			Assert.AreEqual("",
				responseString);
		}

		[TestMethod]
		public async Task PostReportHop()
		{
			// Act
			var response = await _client.GetAsync("/api/parcel/{trackingId}/reportHop/{code}");
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();

			// Assert
			Assert.AreEqual("",
				responseString);
		}
	}
}
