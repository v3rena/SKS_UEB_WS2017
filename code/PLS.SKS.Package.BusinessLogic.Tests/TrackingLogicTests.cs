using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PLS.SKS.Package.BusinessLogic;
using PLS.SKS.Package.DataAccess.Mock;
using System;
using System.Collections.Generic;

namespace PLS.SKS.Package.BusinessLogic.Tests
{
    [TestClass]
    public class TrackingLogicTests
    {
        MockHopArrivalRepository mockHopRepo;
        MockTrackingInformationRepository mockTrackRepo;
        MockParcelRepository mockParcelRepo;
        Entities.Recipient validBLRec;
        Entities.Parcel validBLParcel;
        Entities.TrackingInformation validBLInfo;
        DataAccess.Entities.Recipient validDALRec;
        DataAccess.Entities.Parcel validDALParcel;
        DataAccess.Entities.TrackingInformation validDALInfo;
        IO.Swagger.Models.TrackingInformation validSwagInfo;

        public void Setup()
        {
            mockHopRepo = new MockHopArrivalRepository();
            mockTrackRepo = new MockTrackingInformationRepository();
            mockParcelRepo = new MockParcelRepository(mockTrackRepo);

            SetupBLTrackingInfo();
            SetupDALTrackingInfo();
            SetupSwagTrackingInfo();

            validBLRec = new Entities.Recipient("Tobias", "Test", "Teststraﬂe 9", "A-1140", "Wien");
            validBLParcel = new Entities.Parcel(1.5f, validBLRec);
            validBLParcel.TrackingInformation = validBLInfo;

            validDALRec = new DataAccess.Entities.Recipient("Tobias", "Test", "Teststraﬂe 9", "A-1140", "Wien");
            validDALParcel = new DataAccess.Entities.Parcel(1.5f, validDALRec, 1);
            validDALParcel.TrackingInformation = validDALInfo;
            validDALParcel.TrackingInformationId = 1;
        }

        private void SetupDALTrackingInfo()
        {
            validDALInfo = new DataAccess.Entities.TrackingInformation(DataAccess.Entities.TrackingInformation.StateEnum.InTransportEnum);

            var hop1 = new DataAccess.Entities.HopArrival { DateTime = DateTime.Now, Code = "WH01", Status = "visited", TrackingInformationId = 1 };
            var hop2 = new DataAccess.Entities.HopArrival { DateTime = DateTime.Now.AddDays(1), Code = "WH02", Status = "future", TrackingInformationId = 1 };
            var hop3 = new DataAccess.Entities.HopArrival { DateTime = DateTime.Now.AddDays(2), Code = "WH03", Status = "future", TrackingInformationId = 1 };
            var hop4 = new DataAccess.Entities.HopArrival { DateTime = DateTime.Now.AddDays(3), Code = "TR01", Status = "future", TrackingInformationId = 1 };

            validDALInfo.Id = 1;
            validDALInfo.visitedHops.Add(hop1);
            validDALInfo.futureHops.Add(hop2);
            validDALInfo.futureHops.Add(hop3);
            validDALInfo.futureHops.Add(hop4);
        }
        private void SetupBLTrackingInfo()
        {
            validBLInfo = new Entities.TrackingInformation(Entities.TrackingInformation.StateEnum.InTransportEnum);

            var hop1 = new Entities.HopArrival { DateTime = DateTime.Now, Code = "WH01", Status = "visited" };
            var hop2 = new Entities.HopArrival { DateTime = DateTime.Now.AddDays(1), Code = "WH02", Status = "future" };
            var hop3 = new Entities.HopArrival { DateTime = DateTime.Now.AddDays(2), Code = "WH03", Status = "future" };
            var hop4 = new Entities.HopArrival { DateTime = DateTime.Now.AddDays(3), Code = "TR01", Status = "future" };

            validBLInfo.visitedHops.Add(hop1);
            validBLInfo.futureHops.Add(hop2);
            validBLInfo.futureHops.Add(hop3);
            validBLInfo.futureHops.Add(hop4);
        }
        private void SetupSwagTrackingInfo()
        {
            var hop1 = new IO.Swagger.Models.HopArrival("WH01", DateTime.Now);
            var hop2 = new IO.Swagger.Models.HopArrival("WH02", DateTime.Now.AddDays(1));
            var hop3 = new IO.Swagger.Models.HopArrival("WH03", DateTime.Now.AddDays(2));
            var hop4 = new IO.Swagger.Models.HopArrival("TR01", DateTime.Now.AddDays(3));

            List<IO.Swagger.Models.HopArrival> visited = new List<IO.Swagger.Models.HopArrival>();
            visited.Add(hop1);
            List<IO.Swagger.Models.HopArrival> future = new List<IO.Swagger.Models.HopArrival>();
            future.Add(hop2);
            future.Add(hop3);
            future.Add(hop4);
            validSwagInfo = new IO.Swagger.Models.TrackingInformation(IO.Swagger.Models.TrackingInformation.StateEnum.InTransportEnum, visited, future);
        }


        //public void MethodUnderTest_Scenario_ExpectedOutcome()
        [TestMethod]
        public void TrackArrival_ValidInputArguments_ReturnsTrackingInformation()
        {
            Setup();            

			var mockMapper = new Mock<AutoMapper.IMapper>();
            mockMapper.Setup(m => m.Map<Entities.Parcel>(It.IsAny<DataAccess.Entities.Parcel>())).Returns(validBLParcel);
            mockMapper.Setup(m => m.Map<IO.Swagger.Models.TrackingInformation>(It.IsAny<Entities.TrackingInformation>())).Returns(validSwagInfo);
            var mapper = mockMapper.Object;
			var mockTrackingLogicLogger = new Mock<ILogger<TrackingLogic>>();
			ILogger<TrackingLogic> trackingLogicLogger = mockTrackingLogicLogger.Object;

			TrackingLogic trLogic = new TrackingLogic(mockParcelRepo, mockHopRepo, mockTrackRepo, trackingLogicLogger, mapper);

            IO.Swagger.Models.TrackingInformation swagTrInfo = trLogic.TrackParcel("TN000001");

            Assert.IsNotNull(swagTrInfo);
		}

		[TestMethod]
		public void TrackArrival_PackageDoesNotExist_ThrowsException()
		{
            Setup();

			var mockMapper = new Mock<AutoMapper.IMapper>();
			var mapper = mockMapper.Object;
			var mockTrackingLogicLogger = new Mock<ILogger<TrackingLogic>>();
			ILogger<TrackingLogic> trackingLogicLogger = mockTrackingLogicLogger.Object;

			TrackingLogic trLogic = new TrackingLogic(mockParcelRepo, mockHopRepo, mockTrackRepo, trackingLogicLogger, mapper);
            Assert.ThrowsException<BLException>(() => trLogic.TrackParcel("12"));
            
		}
	}
}


	