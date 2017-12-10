using System;

namespace PLS.SKS.Package.BusinessLogic.Entities
{
    public class HopArrival
    {
        public HopArrival() { }

        public HopArrival(string code, DateTime dateTime)
        {
            Code = code;
            DateTime = dateTime;
        }

        public string Code { get; set; }
        public DateTime DateTime { get; set; }
		public string Status { get; set; }
		//public virtual TrackingInformation TrackingInformation { get; set; }
	}
}
