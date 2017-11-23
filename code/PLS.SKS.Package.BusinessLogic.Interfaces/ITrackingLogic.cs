namespace PLS.SKS.Package.BusinessLogic.Interfaces
{
    public interface ITrackingLogic
    {
        DataAccess.Entities.Parcel TrackParcel(string trackingID);
    }
}