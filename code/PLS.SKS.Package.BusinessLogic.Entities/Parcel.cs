using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic.Entities
{
    public class Parcel
    {
        public Parcel(int weight, Recipient recipient)
        {
            this.weight = weight;
            this.recipient = recipient;
        }

        private int weight;

        private Recipient recipient;
    }
}
