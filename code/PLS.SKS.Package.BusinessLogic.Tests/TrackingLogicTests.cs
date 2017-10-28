using Microsoft.VisualStudio.TestTools.UnitTesting;
using PLS.SKS.Package.BusinessLogic;

namespace PLS.SKS.Package.BusinessLogic.Tests
{
    [TestClass]
    public class TrackingLogicTests
    {
		//public void MethodUnderTest_Scenario_ExpectedOutcome()
		[TestMethod]
        public void TrackArrival_ValidInputArguments_ReturnsTrue()
        {
			var track = new TrackingLogic();
			track.trackPackage("XYZ");
			Assert.Fail();
		}

		[TestMethod]
		public void TrackArrival_NullPackage_ThrowsException()
		{
			var track = new TrackingLogic();
			track.trackPackage(null);
			Assert.Fail();
		}

	}
}


	