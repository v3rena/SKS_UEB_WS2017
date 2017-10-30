using System;

namespace PLS.SKS.Package.DataAccess.Entities
{
    public class Parcel
    {
        public int weight { get; set; }
        public string trackingNumber { get; set; }
        public Recipient recipient { get; set; }
    }
}
