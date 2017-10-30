using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic.Entities
{
    public class Recipient
    {
        public Recipient()
        {

        }
        public Recipient(string firstName, string lastName, string street, string postalCode, string city)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.street = street;
            this.postalCode = postalCode;
            this.city = city;
        }

        public string firstName { get; set; }
		public string lastName { get; set; }
		public string street { get; set; }
		public string postalCode { get; set; }
		public string city { get; set; }

    }
}
