using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Entities
{
    public class HopArrival
    {
        public HopArrival() { }

        public HopArrival(string code, DateTime dateTime)
        {
            this.code = code;
            this.dateTime = dateTime;
        }

        public int id { get; set; }
        public string code { get; set; }
        public DateTime dateTime { get; set; }
    }
}
