using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PLS.SKS.Package.DataAccess.Entities;

namespace PLS.SKS.Package.DataAccess.Sql
{
    public class DBContext : DbContext
	{
		public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
			//this.Database.EnsureCreated();
		}

		public DbSet<Recipient> Recipients { get; set; }
		public DbSet<Parcel> Parcels { get; set; }
		public DbSet<TrackingInformation> TrackingInformations { get; set; }
		public DbSet<HopArrival> HopArrivals { get; set; }
		public DbSet<Truck> Trucks { get; set; }
		public DbSet<Warehouse> Warehouses { get; set; }
	}
}
