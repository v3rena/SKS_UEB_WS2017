using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Entities
{
    public class HopArrival
    {
        public HopArrival() { }

        public HopArrival(string code, DateTime dateTime)
        {
            this.code = code;
            this.dateTime = dateTime;
        }

        public int id { get; set; }
        public string code { get; set; } //Code of Warehouse or Truck
        public DateTime dateTime { get; set; }
		//public int trackingInformationId { get; set; }
		//public virtual TrackingInformation trackingInformation { get; set; }
    }
}
