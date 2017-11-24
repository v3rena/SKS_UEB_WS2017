using System;
using System.Collections.Generic;
using System.Text;
using IO.Swagger.Models;

namespace PLS.SKS.Package.BusinessLogic.Interfaces
{
    public interface IBusinessLogicFacade
    {
        void ScanParcel(string trackingNumber, string code);
        string AddParcel(Parcel parcel);
        TrackingInformation TrackParcel(string trackingNumber);
        Warehouse ExportWarehouses();
		void ImportWarehouses(Warehouse warehouseRoot);
	}
}
