using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Entities
{
    public class Truck : Hop
    {
        public int id { get; set; }
        public string numberPlate { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public decimal radius { get; set; }
    }
}
