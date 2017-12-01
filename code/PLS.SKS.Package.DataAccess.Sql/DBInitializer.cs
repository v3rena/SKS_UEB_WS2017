using PLS.SKS.Package.DataAccess.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PLS.SKS.Package.DataAccess.Sql
{
    public class DBInitializer
    {
		public static void Initialize(DBContext context)
		{
			context.Database.EnsureCreated();

			// Look for any warehouses
			if (context.Warehouses.Any())
			{
				return;   // DB has been seeded
			}

			var recipients = new Recipient[]
			{
				new Recipient{FirstName="Turanga",LastName="Leela", Street="Schlagergasse 1", City="Wien", PostalCode="A-1090"},
				new Recipient{FirstName="Hubert",LastName="Farnsworth", Street="Donatusgasse 2", City="Linz", PostalCode="A-4020"}
			};
			foreach (Recipient s in recipients)
			{
				context.Recipients.Add(s);
			}
			context.SaveChanges();

			var trucks = new Truck[]
			{
				new Truck{Code="TR01", Duration=1m, Latitude=48.2089816m, Longitude=16.373213299999975m, Radius=30m, NumberPlate="WR-2765"}, //Wien Stephansplatz
				new Truck{Code="TR02", Duration=1m, Latitude=48.2089816m, Longitude=16.373213299999975m, Radius=30m, NumberPlate="WR-2788"}, //Wien Stephansplatz
			};
			var trucks2 = new Truck[]
			{
				new Truck{Code="TR03", Duration=1m, Latitude=48.3059826m, Longitude=14.287141199999951m, Radius=30m, NumberPlate="WR-2777"}, //Linz Hauptplatz
				new Truck{Code="TR04", Duration=1m, Latitude=48.85661400000001m, Longitude=2.3522219000000177m, Radius=30m, NumberPlate="WR-2739"}, //Paris
			};
			foreach (Truck c in trucks)
			{
				context.Trucks.Add(c);
			}
			foreach (Truck c in trucks2)
			{
				context.Trucks.Add(c);
			}
			context.SaveChanges();

			var w01 = new Warehouse { Code = "WH01", Description = "Superwarehouse 01", Duration = 2m, Trucks = new List<Truck>(), NextHops = new List<Warehouse>() };
			var w02 = new Warehouse { Code = "WH02", Description = "Warehouse 02", Duration = 3m, Trucks = new List<Truck>(), NextHops = new List<Warehouse>() };
			var w03 = new Warehouse { Code = "WH03", Description = "Warehouse 03", Duration = 1m, Trucks = trucks.ToList(), NextHops = new List<Warehouse>() };
			var w04 = new Warehouse { Code = "WH04", Description = "Warehouse 04", Duration = 2m, Trucks = trucks2.ToList(), NextHops = new List<Warehouse>() };

			w01.NextHops.Add(w02);
			w02.NextHops.Add(w03);
			w02.NextHops.Add(w04);
			var warehouses = new Warehouse[]
			{
				w01,
				w02,
				w03,
				w04
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
				new HopArrival{DateTime=DateTime.Parse("2017-11-09"), Code="WH01", Status = "visited", TrackingInformationId=1},
                new HopArrival{DateTime=DateTime.Parse("2017-11-10"), Code="WH02", Status = "visited", TrackingInformationId=1},
                new HopArrival{DateTime=DateTime.Parse("2017-11-11"), Code="WH03", Status = "future", TrackingInformationId=1},
                new HopArrival{DateTime=DateTime.Parse("2017-11-12"), Code="TR01", Status = "future", TrackingInformationId=1},
                new HopArrival{DateTime=DateTime.Parse("2018-10-02"), Code="WH01", Status = "future", TrackingInformationId=2},
				new HopArrival{DateTime=DateTime.Parse("2018-10-03"), Code="WH02", Status = "future", TrackingInformationId=2},
				new HopArrival{DateTime=DateTime.Parse("2018-10-04"), Code="WH03", Status = "future", TrackingInformationId=2},
				new HopArrival{DateTime=DateTime.Parse("2018-10-05"), Code="TR01", Status = "future", TrackingInformationId=2},
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
