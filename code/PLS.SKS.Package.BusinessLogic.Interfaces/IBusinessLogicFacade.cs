using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic.Interfaces
{
    public interface IBusinessLogicFacade
    {
        void ScanParcel(string trackingNumber, string code);
        string AddParcel(IO.Swagger.Models.Parcel parcel);
        IO.Swagger.Models.TrackingInformation TrackParcel(string trackingNumber);
        IO.Swagger.Models.Warehouse ExportWarehouses();
        void CreateMaps();
    }
}
