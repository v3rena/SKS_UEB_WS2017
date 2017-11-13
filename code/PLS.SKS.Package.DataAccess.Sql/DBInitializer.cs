using PLS.SKS.Package.DataAccess.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Sql
{
    public class DBInitializer
    {
		public static void Initialize(DBContext context)
		{
			context.Database.EnsureCreated();

			// Look for any recipients.
			if (context.Recipients.Any())
			{
				return;   // DB has been seeded
			}

			var recipients = new Recipient[]
			{
				new Recipient{firstName="Carson",lastName="Alexander", street="Teststrasse 1", city="Wien", postalCode="1010"},
				new Recipient{firstName="Test",lastName="Tobias", street="Teststrasse 1", city="Wien", postalCode="1010"}
			};
			foreach (Recipient s in recipients)
			{
				context.Recipients.Add(s);
			}
			context.SaveChanges();

			var trucks = new Truck[]
			{
				new Truck{code="TR01", duration=1.3m, latitude=1.1m, longitude=2.0m, radius=3.3m, numberPlate="WR-2765"},
				new Truck{code="TR02", duration=1.3m, latitude=1.1m, longitude=2.0m, radius=3.3m, numberPlate="WR-2788"}
			};
			foreach (Truck c in trucks)
			{
				context.Trucks.Add(c);
			}
			context.SaveChanges();

			var warehouses = new Warehouse[]
			{
				new Warehouse{code="WH01", description="Warehouse Super 01", duration=1.5m, trucks=trucks.ToList(), nextHops=new List<Warehouse>()},
				new Warehouse{code="WH01", description="Warehouse Super 01", duration=1.5m, trucks=trucks.ToList(), nextHops=new List<Warehouse>()}
			};
			foreach (Warehouse e in warehouses)
			{
				context.Warehouses.Add(e);
			}
			context.SaveChanges();

			var trackingInformations = new TrackingInformation[]
			{
				new TrackingInformation{state="InTransport", futureHops=new List<HopArrival>(), visitedHops=new List<HopArrival>()},
				new TrackingInformation{state="InTransport", futureHops=new List<HopArrival>(), visitedHops=new List<HopArrival>()}
			};
			foreach (TrackingInformation e in trackingInformations)
			{
				context.TrackingInformations.Add(e);
			}
			context.SaveChanges();

			var hopArrivals = new HopArrival[]
			{
				new HopArrival{dateTime=DateTime.Parse("2017-11-09"), code="WH01", trackingInformationId=1},
				new HopArrival{dateTime=DateTime.Parse("2017-11-10"), code="WH02", trackingInformationId=2}
			};
			foreach (HopArrival e in hopArrivals)
			{
				context.HopArrivals.Add(e);
			}
			context.SaveChanges();

			var parcels = new Parcel[]
			{
				new Parcel{recipientId=1, weight=22, trackingNumber="TN01", trackingInformationId=1},
				new Parcel{recipientId=2, weight=22, trackingNumber="TN02", trackingInformationId=2},
			};
			foreach (Parcel e in parcels)
			{
				context.Parcels.Add(e);
			}
			context.SaveChanges();
		}
	}
}
