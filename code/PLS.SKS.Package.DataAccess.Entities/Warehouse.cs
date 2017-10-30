using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Entities
{
    public class Warehouse : Hop
    {
        public string description { get; set; }
        public List<Warehouse> nextHops { get; set; }
        public List<Truck> trucks { get; set; }
    }
}
