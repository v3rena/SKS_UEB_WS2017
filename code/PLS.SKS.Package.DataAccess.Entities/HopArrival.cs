using System;

namespace PLS.SKS.Package.DataAccess.Entities
{
    public class HopArrival
    {
        public HopArrival() { }

        public HopArrival(string code, DateTime dateTime, int trId, string status)
        {
            Code = code;
            DateTime = dateTime;
            TrackingInformationId = trId;
            Status = status;
        }

        public int Id { get; set; }
        public string Code { get; set; } //Code of Warehouse or Truck
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
		public int TrackingInformationId { get; set; }
		public virtual TrackingInformation TrackingInformation { get; set; }
    }
}
