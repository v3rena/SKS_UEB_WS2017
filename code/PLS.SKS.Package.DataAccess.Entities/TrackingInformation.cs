using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Entities
{
    public class TrackingInformation
    {
        public TrackingInformation() { }

        public TrackingInformation(StateEnum state, List<HopArrival> futureHops)
        {
            State = state;
            this.futureHops = futureHops;
            visitedHops = new List<HopArrival>();
        }

		public enum StateEnum
		{
			[EnumMember(Value = "InTransport")]
			InTransportEnum,

			[EnumMember(Value = "InTruckDelivery")]
			InTruckDeliveryEnum,

			[EnumMember(Value = "Delivered")]
			DeliveredEnum
		}

		public int Id { get; set; }
		public StateEnum? State { get; set; }
		public List<HopArrival> visitedHops = new List<HopArrival>();
        public List<HopArrival> futureHops = new List<HopArrival>();

        public void AddVisited(HopArrival visited)
        {

        }

        public void RemoveFuture(HopArrival future)
        {

        }
    }
}
