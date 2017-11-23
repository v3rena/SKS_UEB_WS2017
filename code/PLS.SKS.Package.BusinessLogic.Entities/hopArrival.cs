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
            this.Code = code;
            this.DateTime = dateTime;
        }

        public string Code { get; set; }
        public DateTime DateTime { get; set; }
		//public TrackingInformation trackingInformation { get; set; }
	}
}
