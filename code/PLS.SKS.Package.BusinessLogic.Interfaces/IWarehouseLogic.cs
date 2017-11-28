using PLS.SKS.Package.DataAccess.Entities;

namespace PLS.SKS.Package.BusinessLogic.Interfaces
{
	public interface IWarehouseLogic
	{
		IO.Swagger.Models.Warehouse ExportWarehouses();
		void ImportWarehouses(IO.Swagger.Models.Warehouse warehouse);
	}
}