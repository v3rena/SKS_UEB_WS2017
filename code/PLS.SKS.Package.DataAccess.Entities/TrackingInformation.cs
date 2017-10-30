using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Entities
{
    public class TrackingInformation
    {
        public TrackingInformation() { }

        public TrackingInformation(string state, List<HopArrival> futureHops)
        {
            this.state = state;
            this.futureHops = futureHops;
            visitedHops = new List<HopArrival>();
        }

        public int id { get; set; }
        public string state { get; set; }
        public List<HopArrival> visitedHops = new List<HopArrival>();
        public List<HopArrival> futureHops = new List<HopArrival>();

        public void addVisited(HopArrival visited)
        {

        }

        public void removeFuture(HopArrival future)
        {

        }
    }
}
