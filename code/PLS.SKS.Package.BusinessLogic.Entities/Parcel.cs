using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic.Entities
{
    public class Parcel
    {
        public Parcel()
        {
            Recipient = new Recipient();
        }

        public Parcel(float weight, Recipient recipient)
        {
            this.Weight = weight;
            this.Recipient = recipient;
        }

        public float Weight { get; set; }
		public string TrackingNumber { get; set; } //generated in business logic after parcel is successfully posted
		public Recipient Recipient { get; set; }
		public TrackingInformation TrackingInformation { get; set; }
    }
}
