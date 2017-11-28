﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PLS.SKS.Package.BusinessLogic;
using Microsoft.Extensions.DependencyInjection;
using PLS.SKS.Package.DataAccess.Mock;
using System.Collections.Generic;
using Moq;
using Microsoft.Extensions.Logging;

namespace PLS.SKS.Package.BusinessLogic.Tests
{
	[TestClass]
	public class HopArrivalLogicTests
	{
		//public void MethodUnderTest_Scenario_ExpectedOutcome()
		[TestMethod]
		public void ScanParcel_ValidTrNumber_HopArrivalUpdated()
		{
            MockHopArrivalRepository mockHopRepo = new MockHopArrivalRepository();
            MockTrackingInformationRepository mockTrackRepo = new MockTrackingInformationRepository();
            MockParcelRepository mockParcelRepo = new MockParcelRepository(mockTrackRepo);

			var mockMapper = new Mock<AutoMapper.IMapper>();
			var mapper = mockMapper.Object;
			var mockHopArrivalLogicLogger = new Mock<ILogger<HopArrivalLogic>>();
			ILogger<HopArrivalLogic> hopArrivalLogicLogger = mockHopArrivalLogicLogger.Object;

			Interfaces.IHopArrivalLogic parcelLogic = new HopArrivalLogic(mockParcelRepo, mockTrackRepo, mockHopRepo, hopArrivalLogicLogger, mapper);
            parcelLogic.ScanParcel("TN000001", "WH02");

            List<DataAccess.Entities.HopArrival> hopArr = mockHopRepo.GetByTrackingInformationId(1);
            DataAccess.Entities.HopArrival h = new DataAccess.Entities.HopArrival { Code = "WH02" };
            int index = hopArr.FindIndex(a => a.Code == h.Code);

            Assert.AreEqual(hopArr[index].Status, "visited");
		}
	}
}


