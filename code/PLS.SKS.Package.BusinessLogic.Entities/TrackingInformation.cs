using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic.Entities
{
    public class TrackingInformation
    {
        public TrackingInformation(string state, List<hopArrival> futureHops)
        {
            this.state = state;
            this.futureHops = futureHops;
            visitedHops = new List<hopArrival>();
        }

        private string state { get; set; }
        private List<hopArrival> visitedHops;
        private List<hopArrival> futureHops;

        public void addVisited(hopArrival visited)
        {

        }

        public void removeFuture(hopArrival future)
        {

        }
    }
}
