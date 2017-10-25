using Microsoft.VisualStudio.TestTools.UnitTesting;
using PLS.SKS.Package.BusinessLogic;

namespace PLS.SKS.Package.BusinessLogic.Tests
{
	[TestClass]
	public class ParcelEntryLogicTests
	{
		//public void MethodUnderTest_Scenario_ExpectedOutcome()
		[TestMethod]
		public void AddParcel_()
		{
			var parcelEntry = new ParcelEntryLogic();
			parcelEntry.addParcel(new Entities.Parcel(12, new Entities.Recipient("Tobias", "Test", "Teststraße 9", "1140", "Wien")));
			Assert.Fail();
		}
	}
}


