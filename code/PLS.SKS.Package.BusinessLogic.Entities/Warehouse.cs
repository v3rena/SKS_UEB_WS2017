using System.Collections.Generic;

namespace PLS.SKS.Package.BusinessLogic.Entities
{
    public class Warehouse
    {
        public Warehouse() { }

        public Warehouse(string code, string description, decimal duration, List<Warehouse> nextHops, List<Truck> trucks)
        {
            Code = code;
            Description = description;
            Duration = duration;
            NextHops = nextHops;
            Trucks = trucks;
        }

        public string Code { get; set; }
		public string Description { get; set; }
		public decimal Duration { get; set; }
		public List<Warehouse> NextHops { get; set; }
		public List<Truck> Trucks { get; set; }
    }
}
