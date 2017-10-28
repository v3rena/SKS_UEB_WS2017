using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic.Entities
{
    public class Warehouse
    {
        public Warehouse()
        {

        }

		public string code;
		public string description;
		public decimal duration;
		public List<Warehouse> nextHops;
		public List<Truck> trucks;
    }
}
