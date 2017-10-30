using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic.Entities
{
    public class Parcel
    {
        public Parcel()
        {
            recipient = new Recipient();
        }

        public Parcel(int weight, Recipient recipient)
        {
            this.weight = weight;
            this.recipient = recipient;
        }

        public int weight { get; set; }
        public Recipient recipient { get; set; }
    }
}
