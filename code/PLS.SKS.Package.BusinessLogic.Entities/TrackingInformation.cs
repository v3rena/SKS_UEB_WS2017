using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic.Entities
{
    public class TrackingInformation
    {
        public TrackingInformation() { }

        public TrackingInformation(StateEnum state, List<HopArrival> futureHops)
        {
            State = state;
            this.FutureHops = futureHops;
            VisitedHops = new List<HopArrival>();
        }

		public TrackingInformation(StateEnum state)
		{
			State = state;
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

		public StateEnum State { get; set; }
		public List<HopArrival> VisitedHops = new List<HopArrival>();
		public List<HopArrival> FutureHops = new List<HopArrival>();

        public void AddVisited(HopArrival visited)
        {

        }

        public void RemoveFuture(HopArrival future)
        {

        }
    }
}
