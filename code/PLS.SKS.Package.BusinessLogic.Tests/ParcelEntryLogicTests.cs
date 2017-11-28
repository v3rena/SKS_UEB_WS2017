using Microsoft.VisualStudio.TestTools.UnitTesting;
using PLS.SKS.Package.BusinessLogic;
using Moq;
using PLS.SKS.Package.DataAccess.Mock;
using Microsoft.Extensions.Logging;
using PLS.SKS.ServiceAgents;

namespace PLS.SKS.Package.BusinessLogic.Tests
{
	[TestClass]
	public class ParcelEntryLogicTests
	{
        Entities.Recipient validBLRec;
        Entities.Parcel validBLParcel;
        DataAccess.Entities.Recipient validDALRec;
        DataAccess.Entities.Parcel validDALParcel;

        private void Setup()
        {
            validDALRec = new DataAccess.Entities.Recipient("Tobias", "Test", "Teststraße 9", "1140", "Wien");
            validDALParcel = new DataAccess.Entities.Parcel(1.5f, validDALRec, 1);
        }

		//public void MethodUnderTest_Scenario_ExpectedOutcome()
		[TestMethod]
		public void AddParcel_ValidParcel_ParcelAddedSuccesfully()
		{
            Setup();

            MockHopArrivalRepository mockHopRepo = new MockHopArrivalRepository();
            MockTrackingInformationRepository mockTrackRepo = new MockTrackingInformationRepository();
			MockTruckRepository mockTruckRepo = new MockTruckRepository();
            MockParcelRepository mockParcelRepo = new MockParcelRepository(mockTrackRepo);

			var mockMapper = new Mock<AutoMapper.IMapper>();
			var mapper = mockMapper.Object;
			var mockParcelEntryLogicLogger = new Mock<ILogger<ParcelEntryLogic>>();
			ILogger<ParcelEntryLogic> parcelEntryLogicLogger = mockParcelEntryLogicLogger.Object;
			var mockGoogleEncodingAgentLogger = new Mock<ILogger<GoogleEncodingAgent>>();
			ILogger<GoogleEncodingAgent> googleEncodingAgentLogger = mockGoogleEncodingAgentLogger.Object;
			var encodingAgent = new GoogleEncodingAgent(googleEncodingAgentLogger, mapper);

			Interfaces.IParcelEntryLogic parcelLogic = new ParcelEntryLogic(mockTruckRepo, mockParcelRepo, mockTrackRepo, mockHopRepo, encodingAgent, parcelEntryLogicLogger, mapper);

            var trackNumber = parcelLogic.AddParcel(validDALParcel);
            Assert.IsNotNull(trackNumber);
            Assert.AreEqual(trackNumber.Length, 8);
		}
	}
}


