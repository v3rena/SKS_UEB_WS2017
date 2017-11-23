using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic.Entities
{
    public class HopArrival
    {
        public HopArrival() { }

        public HopArrival(string code, DateTime dateTime)
        {
            this.code = code;
            this.dateTime = dateTime;
        }

        public string code { get; set; }
        public DateTime dateTime { get; set; }
		//public TrackingInformation trackingInformation { get; set; }
	}
}
