﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PLS.SKS.Package.DataAccess.Entities
{
	public class TrackingInformation
    {
        public TrackingInformation() { }

        public TrackingInformation(StateEnum state, List<HopArrival> futureHops)
        {
            State = state;
            FutureHops = futureHops;
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

		public int Id { get; set; }
		public StateEnum? State { get; set; }
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
