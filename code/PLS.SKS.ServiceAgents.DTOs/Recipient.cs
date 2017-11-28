using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLS.SKS.ServiceAgents.DTOs
{
	public class Recipient
	{
		public Recipient() {}

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
