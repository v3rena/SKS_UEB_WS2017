using PLS.SKS.Package.DataAccess.Entities;

namespace PLS.SKS.Package.BusinessLogic.Interfaces
{
	public interface IWarehouseLogic
	{
		Warehouse ExportWarehouses();
		void ImportWarehouses(Warehouse warehouse);
	}
}