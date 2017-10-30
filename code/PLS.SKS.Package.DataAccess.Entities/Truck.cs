using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Entities
{
    public class Truck : Hop
    {
        public Truck() { }

        public Truck(string code, string numberPlate, decimal latitude, decimal longitude, decimal radius, decimal duration)
        {
            this.code = code;
            this.numberPlate = numberPlate;
            this.latitude = latitude;
            this.longitude = longitude;
            this.radius = radius;
            this.duration = duration;
        }

        public string numberPlate { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public decimal radius { get; set; }
    }
}
