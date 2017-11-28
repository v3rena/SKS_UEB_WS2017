using PLS.SKS.Package.BusinessLogic.Entities;
using PLS.SKS.Package;

namespace PLS.SKS.Package.BusinessLogic.Interfaces
{
    public interface IParcelEntryLogic
    {
        string AddParcel(IO.Swagger.Models.Parcel parcel);
    }
}