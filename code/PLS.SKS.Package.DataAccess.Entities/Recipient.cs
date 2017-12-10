namespace PLS.SKS.Package.DataAccess.Entities
{
    public class Recipient
    {
        public Recipient() { }
        
        public Recipient(string firstName, string lastName, string street, string postalCode, string city)
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            PostalCode = postalCode;
            City = city;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
    }
}
