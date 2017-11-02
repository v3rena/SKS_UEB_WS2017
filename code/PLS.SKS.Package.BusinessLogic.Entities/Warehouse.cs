using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic.Entities
{
    public class Warehouse
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

        public string code { get; set; }
		public string description { get; set; }
		public decimal duration { get; set; }
		public List<Warehouse> nextHops { get; set; }
		public List<Truck> trucks { get; set; }
    }
}
