using Microsoft.VisualStudio.TestTools.UnitTesting;
using PLS.SKS.Package.BusinessLogic;
using Moq;
using PLS.SKS.Package.DataAccess.Mock;

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
            validDALParcel = new DataAccess.Entities.Parcel(1.5f, validDALRec);
        }

		//public void MethodUnderTest_Scenario_ExpectedOutcome()
		[TestMethod]
		public void AddParcel_ValidParcel_ParcelAddedSuccesfully()
		{
            Setup();

            MockParcelRepository mockParcelRepo = new MockParcelRepository();
            MockHopArrivalRepository mockHopRepo = new MockHopArrivalRepository();
            MockTrackingInformationRepository mockTrackRepo = new MockTrackingInformationRepository();


            Interfaces.IParcelEntryLogic parcelLogic = new ParcelEntryLogic(mockParcelRepo, mockTrackRepo, mockHopRepo);

            var trackNumber = parcelLogic.AddParcel(validDALParcel);
            Assert.IsNotNull(trackNumber);
            Assert.AreEqual(trackNumber.Length, 8);
		}
	}
}


