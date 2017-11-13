using PLS.SKS.Package.BusinessLogic.Entities;

namespace PLS.SKS.Package.BusinessLogic.Interfaces
{
    public interface IHopArrivalLogic
    {
        void scanParcel(DataAccess.Entities.Parcel parcel, string code);
    }
}