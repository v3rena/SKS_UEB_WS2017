using Microsoft.VisualStudio.TestTools.UnitTesting;
using PLS.SKS.Package.BusinessLogic;
using Microsoft.Extensions.DependencyInjection;
using PLS.SKS.Package.DataAccess.Mock;


namespace PLS.SKS.Package.BusinessLogic.Tests
{
	[TestClass]
	public class HopArrivalLogicTests
	{
		//public void MethodUnderTest_Scenario_ExpectedOutcome()
		[TestMethod]
		public void ScanParcel_()
		{
            MockParcelRepository mockParcelRepo = new MockParcelRepository();
            MockHopArrivalRepository mockHopRepo = new MockHopArrivalRepository();
            MockTrackingInformationRepository mockTrackRepo = new MockTrackingInformationRepository();
            //var hopLogic = new HopArrivalLogic();
            //hopArrival.scanParcel(new Entities.Parcel(12, new Entities.Recipient("Tobias", "Test", "Teststraße 9", "1140", "Wien")), "1234");
            Assert.IsTrue(true);
		}
	}
}


