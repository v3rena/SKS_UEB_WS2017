using System;

namespace PLS.SKS.Package.DataAccess.Entities
{
    public class Parcel
    {
        public Parcel()
        {
            recipient = new Recipient();
        }

        public Parcel(float weight, Recipient recipient)
        {
            this.weight = weight;
            this.recipient = recipient;
        }

        public int id { get; set; }
        public float weight { get; set; }
        public string trackingNumber { get; set; }
        public Recipient recipient { get; set; }
    }
}
