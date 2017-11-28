namespace PLS.SKS.Package.BusinessLogic.Interfaces
{
    public interface ITrackingLogic
    {
		IO.Swagger.Models.TrackingInformation TrackParcel(string trackingID);
    }
}