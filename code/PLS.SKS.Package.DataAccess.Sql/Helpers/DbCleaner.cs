using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Sql
{
    public class DbCleaner : IDbCleaner
    {
		private readonly DBContext db;
		ILogger<DbCleaner> logger;

		public DbCleaner(DBContext context, ILogger<DbCleaner> logger)
		{
			db = context;
			this.logger = logger;
		}

		public void CleanDb()
		{
			try
			{
				db.Database.ExecuteSqlCommand("ALTER TABLE [Recipients] NOCHECK CONSTRAINT ALL");
				db.Database.ExecuteSqlCommand("DELETE FROM [Recipients]");
				db.Database.ExecuteSqlCommand("ALTER TABLE [Recipients] WITH CHECK CHECK CONSTRAINT ALL");
				db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[Recipients]', RESEED, 0)");

				db.Database.ExecuteSqlCommand("ALTER TABLE [Trucks] NOCHECK CONSTRAINT ALL");
				db.Database.ExecuteSqlCommand("DELETE FROM [Trucks]");
				db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[Trucks]', RESEED, 0)");
				db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[Trucks]', RESEED, 0)");

				db.Database.ExecuteSqlCommand("ALTER TABLE [Warehouses] NOCHECK CONSTRAINT ALL");
				db.Database.ExecuteSqlCommand("DELETE FROM [Warehouses]");
				db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[Warehouses]', RESEED, 0)");
				db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[Warehouses]', RESEED, 0)");

				db.Database.ExecuteSqlCommand("ALTER TABLE [HopArrivals] NOCHECK CONSTRAINT ALL");
				db.Database.ExecuteSqlCommand("DELETE FROM [HopArrivals]");
				db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[HopArrivals]', RESEED, 0)");
				db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[HopArrivals]', RESEED, 0)");

				db.Database.ExecuteSqlCommand("ALTER TABLE [TrackingInformations] NOCHECK CONSTRAINT ALL");
				db.Database.ExecuteSqlCommand("DELETE FROM [TrackingInformations]");
				db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[TrackingInformations]', RESEED, 0)");
				db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[TrackingInformations]', RESEED, 0)");

				db.Database.ExecuteSqlCommand("ALTER TABLE [Parcels] NOCHECK CONSTRAINT ALL");
				db.Database.ExecuteSqlCommand("DELETE FROM [Parcels]");
				db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[Parcels]', RESEED, 0)");
				db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('[Parcels]', RESEED, 0)");
			}
			catch (Exception ex)
			{
				logger.LogError("An error occured while cleaning the database", ex);
				throw new DALException("An error occured while cleaning the database", ex);
			}

		}
	}
}
