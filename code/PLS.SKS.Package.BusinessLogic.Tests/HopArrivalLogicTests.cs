using Microsoft.VisualStudio.TestTools.UnitTesting;
using PLS.SKS.Package.BusinessLogic;
using Microsoft.Extensions.DependencyInjection;
using PLS.SKS.Package.DataAccess.Mock;
using System.Collections.Generic;
using Moq;
using Microsoft.Extensions.Logging;
using PLS.SKS.Package.BusinessLogic.Helpers;

namespace PLS.SKS.Package.BusinessLogic.Tests
{
    [TestClass]
    public class HopArrivalLogicTests
    {
        MockHopArrivalRepository mockHopRepo;
        MockTrackingInformationRepository mockTrackRepo;
        MockParcelRepository mockParcelRepo;

        public HopArrivalLogicTests()
        {
            mockHopRepo = new MockHopArrivalRepository();
            mockTrackRepo = new MockTrackingInformationRepository();
            mockParcelRepo = new MockParcelRepository(mockTrackRepo);
        }

        //public void MethodUnderTest_Scenario_ExpectedOutcome()
        [TestMethod]
        public void ScanParcel_ValidInputArguments_HopArrivalUpdated()
        {
            var mockMapper = new Mock<AutoMapper.IMapper>();
            var mapper = mockMapper.Object;
            var mockHopArrivalLogicLogger = new Mock<ILogger<HopArrivalLogic>>();
            ILogger<HopArrivalLogic> hopArrivalLogicLogger = mockHopArrivalLogicLogger.Object;
            Interfaces.IHopArrivalLogic hopArrivalLogic = new HopArrivalLogic(mockParcelRepo, mockTrackRepo, mockHopRepo, hopArrivalLogicLogger, mapper);

            hopArrivalLogic.ScanParcel("TN000001", "WH02");

            List<DataAccess.Entities.HopArrival> hopArr = mockHopRepo.GetByTrackingInformationId(1);
            DataAccess.Entities.HopArrival h = new DataAccess.Entities.HopArrival { Code = "WH02" };
            int index = hopArr.FindIndex(a => a.Code == h.Code);

            Assert.AreEqual(hopArr[index].Status, "visited");
        }

        [TestMethod]
        public void ScanParcel_InvalidTrackinNumber_ThrowsException()
        {
            var mockMapper = new Mock<AutoMapper.IMapper>();
            var mapper = mockMapper.Object;
            var mockHopArrivalLogicLogger = new Mock<ILogger<HopArrivalLogic>>();
            ILogger<HopArrivalLogic> hopArrivalLogicLogger = mockHopArrivalLogicLogger.Object;

            Interfaces.IHopArrivalLogic hopArrivalLogic = new HopArrivalLogic(mockParcelRepo, mockTrackRepo, mockHopRepo, hopArrivalLogicLogger, mapper);

            Assert.ThrowsException<BlException>(() => hopArrivalLogic.ScanParcel("12", "WH02"));
        }

        [TestMethod]
        public void ScanParcel_InvalidHopCode_ThrowsException()
        {
            var mockMapper = new Mock<AutoMapper.IMapper>();
            var mapper = mockMapper.Object;
            var mockHopArrivalLogicLogger = new Mock<ILogger<HopArrivalLogic>>();
            ILogger<HopArrivalLogic> hopArrivalLogicLogger = mockHopArrivalLogicLogger.Object;

            Interfaces.IHopArrivalLogic hopArrivalLogic = new HopArrivalLogic(mockParcelRepo, mockTrackRepo, mockHopRepo, hopArrivalLogicLogger, mapper);

            Assert.ThrowsException<BlException>(() => hopArrivalLogic.ScanParcel("TN000001", "WH06"));
        }
    }
}


