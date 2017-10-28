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

        public string firstName;
		public string lastName;
		public string street;
		public string postalCode;
		public string city;

    }
}
