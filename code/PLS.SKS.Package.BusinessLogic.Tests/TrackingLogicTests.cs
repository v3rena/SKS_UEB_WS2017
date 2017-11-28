using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PLS.SKS.Package.BusinessLogic;
using PLS.SKS.Package.DataAccess.Mock;

namespace PLS.SKS.Package.BusinessLogic.Tests
{
    [TestClass]
    public class TrackingLogicTests
    {
		//public void MethodUnderTest_Scenario_ExpectedOutcome()
		[TestMethod]
        public void TrackArrival_ValidInputArguments_ReturnsTrue()
        {
            MockHopArrivalRepository mockHopRepo = new MockHopArrivalRepository();
            MockTrackingInformationRepository mockTrackRepo = new MockTrackingInformationRepository();
            MockParcelRepository mockParcelRepo = new MockParcelRepository(mockTrackRepo);

			var mockMapper = new Mock<AutoMapper.IMapper>();
			var mapper = mockMapper.Object;
			var mockTrackingLogicLogger = new Mock<ILogger<TrackingLogic>>();
			ILogger<TrackingLogic> trackingLogicLogger = mockTrackingLogicLogger.Object;

			TrackingLogic trLogic = new TrackingLogic(mockParcelRepo, mockHopRepo, mockTrackRepo, trackingLogicLogger, mapper);

            DataAccess.Entities.Parcel dalParcel = trLogic.TrackParcel("TN000001");

            Assert.IsNotNull(dalParcel);
		}

		[TestMethod]
		public void TrackArrival_NullPackage_ThrowsException()
		{
            MockHopArrivalRepository mockHopRepo = new MockHopArrivalRepository();
            MockTrackingInformationRepository mockTrackRepo = new MockTrackingInformationRepository();
            MockParcelRepository mockParcelRepo = new MockParcelRepository(mockTrackRepo);

			var mockMapper = new Mock<AutoMapper.IMapper>();
			var mapper = mockMapper.Object;
			var mockTrackingLogicLogger = new Mock<ILogger<TrackingLogic>>();
			ILogger<TrackingLogic> trackingLogicLogger = mockTrackingLogicLogger.Object;

			TrackingLogic trLogic = new TrackingLogic(mockParcelRepo, mockHopRepo, mockTrackRepo, trackingLogicLogger, mapper);

            
		}
	}
}


	