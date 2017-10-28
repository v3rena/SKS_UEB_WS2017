using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic.Entities
{
    public class Truck
    {
        public Truck(string code, string numberPlate, decimal latitude, decimal longitude, decimal radius, decimal duration)
        {
            this.code = code;
            this.numberPlate = numberPlate;
            this.latitude = latitude;
            this.longitude = longitude;
            this.radius = radius;
            this.duration = duration;
        }

        public string code;
		public string numberPlate;
		public decimal latitude;
		public decimal longitude;
		public decimal radius;
		public decimal duration;
    }
}
