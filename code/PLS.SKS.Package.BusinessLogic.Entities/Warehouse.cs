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

        private string code;
        private string description;
        private decimal duration;
        private List<Warehouse> nextHops;
        private List<Truck> trucks;
    }
}
