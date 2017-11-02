﻿using System;
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

        public Parcel(float weight, Recipient recipient)
        {
            this.weight = weight;
            this.recipient = recipient;
        }

        public float weight { get; set; }
        public Recipient recipient { get; set; }
    }
}
