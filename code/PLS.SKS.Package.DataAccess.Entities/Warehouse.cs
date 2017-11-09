using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Entities
{
    public class Warehouse : Hop
    {
        public Warehouse() { }

        public Warehouse(string code, string description, decimal duration, List<Warehouse> nextHops, List<Truck> trucks)
        {
            this.code = code;
            this.description = description;
            this.duration = duration;
            this.nextHops = nextHops;
            this.trucks = trucks;
        }

		public string description { get; set; }
        public List<Warehouse> nextHops { get; set; }
        public List<Truck> trucks { get; set; }
    }
}
