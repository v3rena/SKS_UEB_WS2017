using PLS.SKS.Package.BusinessLogic.Entities;

namespace PLS.SKS.Package.BusinessLogic.Interfaces
{
    public interface IHopArrivalLogic
    {
        void scanParcel(Parcel parcel, string code);
    }
}