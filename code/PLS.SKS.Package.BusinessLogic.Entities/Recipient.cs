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
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Street = street;
            this.PostalCode = postalCode;
            this.City = city;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

    }
}
