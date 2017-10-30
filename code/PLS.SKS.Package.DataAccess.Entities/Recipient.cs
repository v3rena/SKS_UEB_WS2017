using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Entities
{
    public class Recipient
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string street { get; set; }
        public string postalCode { get; set; }
        public string city { get; set; }
    }
}
