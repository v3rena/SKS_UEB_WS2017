using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Entities
{
    public abstract class Hop
    {
	    protected Hop() { }

	    protected Hop(string code, decimal duration)
        {
            this.Code = code;
            this.Duration = duration;
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public decimal Duration { get; set; }
    }
}
