using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.ServiceAgents.DTOs
{
    public class GeoEncodingRoot
    {
		public List<Result> Results { get; set; }
		public string Status { get; set; }
    }
}
