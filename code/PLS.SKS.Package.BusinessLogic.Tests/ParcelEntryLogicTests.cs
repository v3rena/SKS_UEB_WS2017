using Microsoft.VisualStudio.TestTools.UnitTesting;
using PLS.SKS.Package.BusinessLogic;
using Moq;
using PLS.SKS.Package.DataAccess.Mock;
using Microsoft.Extensions.Logging;
using PLS.SKS.ServiceAgents;
using System;

namespace PLS.SKS.Package.BusinessLogic.Tests
{
	[TestClass]
	public class ParcelEntryLogicTests
	{
        MockHopArrivalRepository mockHopRepo;
        MockTrackingInformationRepository mockTrackRepo;
        MockTruckRepository mockTruckRepo;
        MockParcelRepository mockParcelRepo;
		MockWarehouseRepository mockWarehouseRepo;

        Entities.Recipient validBLRec;
        Entities.Parcel validBLParcel;
        Entities.Parcel invalidBLParcel;
        Entities.Recipient invalidBLRec;
        DataAccess.Entities.Recipient validDALRec;
        DataAccess.Entities.Parcel validDALParcel;
        IO.Swagger.Models.Recipient validSwagRec;
        IO.Swagger.Models.Parcel validSwagParcel;

		ServiceAgents.DTOs.Recipient validSARecipient;
		ServiceAgents.DTOs.Location validSALocation;

        ILogger<ParcelEntryLogic> parcelEntryLogicLogger;
        ILogger<GoogleEncodingAgent> googleEncodingAgentLogger;
        GoogleEncodingAgent encodingAgent;

        public ParcelEntryLogicTests()
        {
            mockHopRepo = new MockHopArrivalRepository();
            mockTrackRepo = new MockTrackingInformationRepository();
            mockTruckRepo = new MockTruckRepository();
			mockWarehouseRepo = new MockWarehouseRepository();
            mockParcelRepo = new MockParcelRepository(mockTrackRepo);

            CreateParcels();

            var mockParcelEntryLogicLogger = new Mock<ILogger<ParcelEntryLogic>>();
            parcelEntryLogicLogger = mockParcelEntryLogicLogger.Object;

            var mockGoogleEncodingAgentLogger = new Mock<ILogger<GoogleEncodingAgent>>();
            googleEncodingAgentLogger = mockGoogleEncodingAgentLogger.Object;            

        }
        private void CreateParcels()
        {
            validSwagRec = new IO.Swagger.Models.Recipient("Tobias", "Test", "Horvathgasse 2", "A-1160", "Wien");
            validSwagParcel = new IO.Swagger.Models.Parcel(1.5f, validSwagRec);
            validBLRec = new Entities.Recipient("Tobias", "Test", "Horvathgasse 2", "A-1160", "Wien");
            validBLParcel = new Entities.Parcel(1.5f, validBLRec);
            invalidBLRec = new Entities.Recipient("Tobias", "Test", "Horvathgasse 2", "1160", "Wien");
            invalidBLParcel = new Entities.Parcel(1.5f, invalidBLRec);
            validDALRec = new DataAccess.Entities.Recipient("Tobias", "Test", "Horvathgasse 2", "A-1160", "Wien");
            validDALParcel = new DataAccess.Entities.Parcel(1.5f, validDALRec, 1);
		}
		
		/*[TestMethod]
		public void AddParcel_ValidInputArguments_ReturnsParcelTrackingNumber()
		{
            //Arrange
            var mockMapper = new Mock<AutoMapper.IMapper>();
            mockMapper.Setup(m => m.Map<Entities.Parcel>(It.IsAny<IO.Swagger.Models.Parcel>())).Returns(validBLParcel);
            mockMapper.Setup(m => m.Map<DataAccess.Entities.Parcel>(It.IsAny<Entities.Parcel>())).Returns(validDALParcel);
			mockMapper.Setup(m => m.Map<Entities.Parcel>(It.IsAny<DataAccess.Entities.Parcel>())).Returns(validBLParcel);
			mockMapper.Setup(m => m.Map<IO.Swagger.Models.Parcel>(It.IsAny<Entities.Parcel>())).Returns(validSwagParcel);

			var mapper = mockMapper.Object;

            encodingAgent = new GoogleEncodingAgent(googleEncodingAgentLogger, mapper);
            Interfaces.IParcelEntryLogic parcelLogic = new ParcelEntryLogic(mockWarehouseRepo, mockTruckRepo, mockParcelRepo, mockTrackRepo, mockHopRepo, encodingAgent, parcelEntryLogicLogger, mapper);
            
			//Act
            var trackNumber = parcelLogic.AddParcel(validSwagParcel);
            //Assert
            Assert.IsNotNull(trackNumber);
            Assert.AreEqual(trackNumber.Length, 8);
		}*/

        [TestMethod]
        public void AddParcel_NullParcel_ThrowsException()
        {
            var mockMapper = new Mock<AutoMapper.IMapper>();
            mockMapper.Setup(m => m.Map<Entities.Parcel>(It.IsAny<IO.Swagger.Models.Parcel>())).Returns(validBLParcel);
            mockMapper.Setup(m => m.Map<DataAccess.Entities.Parcel>(It.IsAny<Entities.Parcel>())).Returns(validDALParcel);
            var mapper = mockMapper.Object;
            encodingAgent = new GoogleEncodingAgent(googleEncodingAgentLogger, mapper);
            Interfaces.IParcelEntryLogic parcelLogic = new ParcelEntryLogic(mockWarehouseRepo, mockTruckRepo, mockParcelRepo, mockTrackRepo, mockHopRepo, encodingAgent, parcelEntryLogicLogger, mapper);

            Assert.ThrowsException<BLException>( () => parcelLogic.AddParcel(null));
        }

        [TestMethod]
        public void AddParcel_InvalidParcel_ThrowsException()
        {
            var mockMapper = new Mock<AutoMapper.IMapper>();
            mockMapper.Setup(m => m.Map<Entities.Parcel>(It.IsAny<IO.Swagger.Models.Parcel>())).Returns(invalidBLParcel);
            mockMapper.Setup(m => m.Map<DataAccess.Entities.Parcel>(It.IsAny<Entities.Parcel>())).Returns(validDALParcel);
            var mapper = mockMapper.Object;
            encodingAgent = new GoogleEncodingAgent(googleEncodingAgentLogger, mapper);
            Interfaces.IParcelEntryLogic parcelLogic = new ParcelEntryLogic(mockWarehouseRepo, mockTruckRepo, mockParcelRepo, mockTrackRepo, mockHopRepo, encodingAgent, parcelEntryLogicLogger, mapper);

            Assert.ThrowsException<BLException>(() => parcelLogic.AddParcel(validSwagParcel));
        }
    }
}


