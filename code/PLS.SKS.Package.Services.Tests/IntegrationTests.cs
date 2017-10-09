using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using PLS.SKS.Package.Services;
using System.Threading.Tasks;

namespace PLS.SKS.Package.Services.Tests
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public async Task GetWarehouse()
        {
			//Arrange
			HttpClient client = new HttpClient();

			//Act
			var response = client.GetStringAsync("http://localhost/api/warehouse");
			string content = await response;

			//Assert
			Assert.AreEqual(content, "404");
		}

		[TestMethod]
		public async Task PostWarehouse()
		{
			//Arrange
			HttpClient client = new HttpClient();

			//Act
			var response = client.GetStringAsync("");
			string content = await response;

			//Assert
			Assert.AreEqual(content, "404");
		}

		[TestMethod]
		public async Task PostParcel()
		{
			//Arrange
			HttpClient client = new HttpClient();

			//Act
			var response = client.GetStringAsync("");
			string content = await response;

			//Assert
			Assert.AreEqual(content, "404");
		}

		[TestMethod]
		public async Task GetParcel()
		{
			//Arrange
			HttpClient client = new HttpClient();

			//Act
			var response = client.GetStringAsync("");
			string content = await response;

			//Assert
			Assert.AreEqual(content, "404");
		}

		[TestMethod]
		public async Task PostReportHop()
		{
			//Arrange
			HttpClient client = new HttpClient();

			//Act
			var response = client.GetStringAsync("");
			string content = await response;

			//Assert
			Assert.AreEqual(content, "404");
		}
	}
}
