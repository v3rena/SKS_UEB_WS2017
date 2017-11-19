using PLS.SKS.Package.DataAccess.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
				new Recipient{firstName="Carson",lastName="Alexander", street="Teststrasse 1", city="Wien", postalCode="A-1010"},
				new Recipient{firstName="Test",lastName="Tobias", street="Teststrasse 2", city="Wien", postalCode="A-1020"}
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

			var w01 = new Warehouse { code = "WH01", description = "Superwarehouse 01", duration = 1.5m, trucks = new List<Truck>(), nextHops = new List<Warehouse>() };
			var w02 = new Warehouse { code = "WH02", description = "Warehouse 02", duration = 1.5m, trucks = new List<Truck>(), nextHops = new List<Warehouse>() };
			var w03 = new Warehouse { code = "WH03", description = "Warehouse 03", duration = 1.5m, trucks = trucks.ToList(), nextHops = new List<Warehouse>() };

			w01.nextHops.Add(w02);
			w02.nextHops.Add(w03);
			var warehouses = new Warehouse[]
			{
				w01,
				w02,
				w03
			};
			foreach (Warehouse e in warehouses)
			{
				context.Warehouses.Add(e);
			}
			context.SaveChanges();

			var trackingInformations = new TrackingInformation[]
			{
				new TrackingInformation{State=TrackingInformation.StateEnum.DeliveredEnum },
				new TrackingInformation{State=TrackingInformation.StateEnum.InTransportEnum }
			};
			foreach (TrackingInformation e in trackingInformations)
			{
				context.TrackingInformations.Add(e);
			}
			context.SaveChanges();

			var hopArrivals = new HopArrival[]
			{
				new HopArrival{dateTime=DateTime.Parse("2017-11-09"), code="WH01", trackingInformationId=1},
				new HopArrival{dateTime=DateTime.Parse("2017-11-10"), code="WH02", trackingInformationId=1},
				new HopArrival{dateTime=DateTime.Parse("2017-11-11"), code="WH03", trackingInformationId=1},
				new HopArrival{dateTime=DateTime.Parse("2017-10-02"), code="WH01", trackingInformationId=2}
			};
			foreach (HopArrival e in hopArrivals)
			{
				context.HopArrivals.Add(e);
			}
			context.SaveChanges();

			var parcels = new Parcel[]
			{
				new Parcel{RecipientId=1, Weight=22, TrackingNumber="TN000001", TrackingInformationId=1},
				new Parcel{RecipientId=2, Weight=10, TrackingNumber="TN000002", TrackingInformationId=2},
			};
			foreach (Parcel e in parcels)
			{
				context.Parcels.Add(e);
			}
			context.SaveChanges();
		}
	}
}
