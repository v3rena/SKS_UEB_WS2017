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

        public Parcel(int weight, Recipient recipient)
        {
            this.Weight = weight;
            this.Recipient = recipient;
        }

        public int Weight { get; set; }

        public Recipient Recipient { get; set; }
    }
}
