using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Entities
{
    public class Hop
    {
        public Hop() { }

        public Hop(string code, decimal duration)
        {
            this.code = code;
            this.duration = duration;
        }

        public int id { get; set; }
        public string code { get; set; }
        public decimal duration { get; set; }
    }
}
