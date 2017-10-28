using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic.Entities
{
    public class TrackingInformation
    {
        public TrackingInformation(string state, List<HopArrival> futureHops)
        {
            this.state = state;
            this.futureHops = futureHops;
            visitedHops = new List<HopArrival>();
        }

		public string state { get; set; }
		public List<HopArrival> visitedHops;
		public List<HopArrival> futureHops;

        public void addVisited(HopArrival visited)
        {

        }

        public void removeFuture(HopArrival future)
        {

        }
    }
}
