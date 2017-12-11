using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Mock;
using Moq;
using Microsoft.Extensions.Logging;
using PLS.SKS.Package.BusinessLogic.Helpers;

namespace PLS.SKS.Package.BusinessLogic.Tests
{
    [TestClass]
    public class WarehouseLogicTests
    {
        private MockWarehouseRepository mockWareRepo;
        private MockTruckRepository mockTruckRepo;
        private IO.Swagger.Models.Warehouse validRoot;
        private IO.Swagger.Models.Warehouse wh2;
        private IO.Swagger.Models.Warehouse invalidRoot;
        private Entities.Warehouse validBLWarehouse;
        private Entities.Warehouse validBLWarehouse2;
        private Entities.Warehouse invalidBLWarehouse;
        private DataAccess.Entities.Warehouse validDALWarehouse;
        private DataAccess.Entities.Warehouse validDALWarehouse2;

        public WarehouseLogicTests()
        {
            mockTruckRepo = new MockTruckRepository();
            wh2 = new IO.Swagger.Models.Warehouse("WH02", "descr", 1.0m, new List<IO.Swagger.Models.Warehouse>(), new List<IO.Swagger.Models.Truck>());
            var rootList = new List<IO.Swagger.Models.Warehouse>();
            rootList.Add(wh2);
            validRoot = new IO.Swagger.Models.Warehouse("WH01", "descr", 1.0m, rootList, new List<IO.Swagger.Models.Truck>());
            invalidRoot = new IO.Swagger.Models.Warehouse("", "descr", 1.0m, new List<IO.Swagger.Models.Warehouse>(), new List<IO.Swagger.Models.Truck>());
            validBLWarehouse2 = new Entities.Warehouse("WH02", "descr", 1.0m, new List<Entities.Warehouse>(), new List<Entities.Truck>());
            var blList = new List<Entities.Warehouse>();
            blList.Add(validBLWarehouse2);
            validDALWarehouse2 = new DataAccess.Entities.Warehouse("WH02", "descr", 1.0m, new List<DataAccess.Entities.Warehouse>(), new List<DataAccess.Entities.Truck>());
            var dalList = new List<DataAccess.Entities.Warehouse>();
            dalList.Add(validDALWarehouse2);

            validBLWarehouse = new Entities.Warehouse("WH01", "descr", 1.0m, blList, new List<Entities.Truck>());
            validDALWarehouse = new DataAccess.Entities.Warehouse("WH01", "descr", 1.0m, dalList, new List<DataAccess.Entities.Truck>());

            invalidBLWarehouse = new Entities.Warehouse("", "descr", 1.0m, new List<Entities.Warehouse>(), new List<Entities.Truck>());
        }

        [TestMethod]
        public void ExportWarehouse_FilledDataBase_Success()
        {
            //Arrange
            mockWareRepo = new MockWarehouseRepository();
            var mockMapper = new Mock<AutoMapper.IMapper>();
            mockMapper.Setup(m => m.Map<Entities.Warehouse>(It.IsAny<DataAccess.Entities.Warehouse>())).Returns(validBLWarehouse);
            mockMapper.Setup(m => m.Map<IO.Swagger.Models.Warehouse>(It.IsAny<Entities.Warehouse>())).Returns(validRoot);
            var mapper = mockMapper.Object;
            var mockHopArrivalLogicLogger = new Mock<ILogger<WarehouseLogic>>();
            ILogger<WarehouseLogic> warehouseLogicLogger = mockHopArrivalLogicLogger.Object;
			var mockCleaner = new Mock<DataAccess.Interfaces.IDbCleaner>();
			var cleaner = mockCleaner.Object;
            Interfaces.IWarehouseLogic warehouseLogic = new WarehouseLogic(mockWareRepo, warehouseLogicLogger, mapper, cleaner);

            //Act
            IO.Swagger.Models.Warehouse rootWarehouse = warehouseLogic.ExportWarehouses();

            //Assert
            Assert.IsNotNull(rootWarehouse);
        }

        [TestMethod]
        public void ExportWarehouse_EmptyDataBase_ThrowsException()
        {
            //Arrange
            mockWareRepo = new MockWarehouseRepository(true);
            var mockMapper = new Mock<AutoMapper.IMapper>();
            var mapper = mockMapper.Object;
            var mockHopArrivalLogicLogger = new Mock<ILogger<WarehouseLogic>>();
            ILogger<WarehouseLogic> warehouseLogicLogger = mockHopArrivalLogicLogger.Object;
			var mockCleaner = new Mock<DataAccess.Interfaces.IDbCleaner>();
			var cleaner = mockCleaner.Object;
			Interfaces.IWarehouseLogic warehouseLogic = new WarehouseLogic(mockWareRepo, warehouseLogicLogger, mapper, cleaner);

            //Act
            //Assert
            Assert.ThrowsException<BlException>(() => warehouseLogic.ExportWarehouses());
        }

        [TestMethod]
        public void ImportWarehouse_ValidInputArguments_FillsDatabase()
        {
            mockWareRepo = new MockWarehouseRepository(true);
            var mockMapper = new Mock<AutoMapper.IMapper>();
            mockMapper.Setup(m => m.Map<Entities.Warehouse>(It.IsAny<IO.Swagger.Models.Warehouse>())).Returns(validBLWarehouse);
            mockMapper.Setup(m => m.Map<DataAccess.Entities.Warehouse>(It.IsAny<Entities.Warehouse>())).Returns(validDALWarehouse);
            var mapper = mockMapper.Object;
            var mockHopArrivalLogicLogger = new Mock<ILogger<WarehouseLogic>>();
            ILogger<WarehouseLogic> warehouseLogicLogger = mockHopArrivalLogicLogger.Object;
			var mockCleaner = new Mock<DataAccess.Interfaces.IDbCleaner>();
			var cleaner = mockCleaner.Object;
			Interfaces.IWarehouseLogic warehouseLogic = new WarehouseLogic(mockWareRepo, warehouseLogicLogger, mapper, cleaner);

            warehouseLogic.ImportWarehouses(validRoot);

            var root = mockWareRepo.GetById(1);

            Assert.IsNotNull(root);
        }

        [TestMethod]
        public void ImportWarehouse_InvalidWarehouse_ThrowsException()
        {
            mockWareRepo = new MockWarehouseRepository(true);
            var mockMapper = new Mock<AutoMapper.IMapper>();
            mockMapper.Setup(m => m.Map<Entities.Warehouse>(It.IsAny<IO.Swagger.Models.Warehouse>())).Returns(invalidBLWarehouse);
            var mapper = mockMapper.Object;
            var mockHopArrivalLogicLogger = new Mock<ILogger<WarehouseLogic>>();
            ILogger<WarehouseLogic> warehouseLogicLogger = mockHopArrivalLogicLogger.Object;
			var mockCleaner = new Mock<DataAccess.Interfaces.IDbCleaner>();
			var cleaner = mockCleaner.Object;
			Interfaces.IWarehouseLogic warehouseLogic = new WarehouseLogic(mockWareRepo, warehouseLogicLogger, mapper, cleaner);

            Assert.ThrowsException<BlException>(() => warehouseLogic.ImportWarehouses(invalidRoot));
        }
    }
}
