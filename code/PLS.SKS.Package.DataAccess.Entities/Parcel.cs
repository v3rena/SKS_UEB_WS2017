﻿using System;

namespace PLS.SKS.Package.DataAccess.Entities
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

        public int Id { get; set; }
        public float Weight { get; set; }
		public int RecipientId { get; set; }
        public Recipient Recipient { get; set; }
		public string TrackingNumber { get; set; } //generated in business logic after parcel is successfully posted
		public int TrackingInformationId { get; set; }
		public TrackingInformation TrackingInformation { get; set; }
	}
}
