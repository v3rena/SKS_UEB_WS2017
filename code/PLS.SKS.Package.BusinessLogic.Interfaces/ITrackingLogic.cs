namespace PLS.SKS.Package.BusinessLogic.Interfaces
{
    public interface ITrackingLogic
    {
        DataAccess.Entities.Parcel trackParcel(string trackingID);
    }
}