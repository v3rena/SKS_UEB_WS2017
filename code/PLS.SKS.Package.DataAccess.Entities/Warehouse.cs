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
            this.Code = code;
            this.Description = description;
            this.Duration = duration;
            this.NextHops = nextHops;
            this.Trucks = trucks;
        }

		public string Description { get; set; }
        public List<Warehouse> NextHops { get; set; }
        public List<Truck> Trucks { get; set; }
    }
}
