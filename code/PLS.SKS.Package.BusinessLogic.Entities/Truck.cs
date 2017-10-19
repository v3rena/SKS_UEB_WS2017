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

        private string code;
        private string numberPlate;
        private decimal latitude;
        private decimal longitude;
        private decimal radius;
        private decimal duration;
    }
}
