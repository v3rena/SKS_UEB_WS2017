﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PLS.SKS.Package.DataAccess.Interfaces;
using PLS.SKS.Package.DataAccess.Sql.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.DataAccess.Sql
{
    public class DbCleaner : IDbCleaner
    {
		private readonly DBContext db;
		private ILogger<DbCleaner> logger;

		public DbCleaner(DBContext context, ILogger<DbCleaner> logger)
		{
			db = context;
			this.logger = logger;
		}

		public virtual void CleanDb()
		{
			try
			{
				db.Database.ExecuteSqlCommand("ALTER TABLE [Recipients] NOCHECK CONSTRAINT ALL");
				db.Database.ExecuteSqlCommand("DELETE FROM [Recipients]");
				db.Database.ExecuteSqlCommand("ALTER TABLE [Recipients] WITH CHECK CHECK CONSTRAINT ALL");
				db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT ('[Recipients]', RESEED, {Constants.DbStartIndex})");

				db.Database.ExecuteSqlCommand("ALTER TABLE [Trucks] NOCHECK CONSTRAINT ALL");
				db.Database.ExecuteSqlCommand("DELETE FROM [Trucks]");
				db.Database.ExecuteSqlCommand("ALTER TABLE [Trucks] WITH CHECK CHECK CONSTRAINT ALL");
				db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT ('[Trucks]', RESEED, {Constants.DbStartIndex})");

				db.Database.ExecuteSqlCommand("ALTER TABLE [Warehouses] NOCHECK CONSTRAINT ALL");
				db.Database.ExecuteSqlCommand("DELETE FROM [Warehouses]");
				db.Database.ExecuteSqlCommand("ALTER TABLE [Warehouses] WITH CHECK CHECK CONSTRAINT ALL");
				db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT ('[Warehouses]', RESEED, {Constants.DbStartIndex})");

				db.Database.ExecuteSqlCommand("ALTER TABLE [HopArrivals] NOCHECK CONSTRAINT ALL");
				db.Database.ExecuteSqlCommand("DELETE FROM [HopArrivals]");
				db.Database.ExecuteSqlCommand("ALTER TABLE [HopArrivals] WITH CHECK CHECK CONSTRAINT ALL");
				db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT ('[HopArrivals]', RESEED, {Constants.DbStartIndex})");

				db.Database.ExecuteSqlCommand("ALTER TABLE [TrackingInformations] NOCHECK CONSTRAINT ALL");
				db.Database.ExecuteSqlCommand("DELETE FROM [TrackingInformations]");
				db.Database.ExecuteSqlCommand("ALTER TABLE [TrackingInformations] WITH CHECK CHECK CONSTRAINT ALL");
				db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT ('[TrackingInformations]', RESEED, {Constants.DbStartIndex})");

				db.Database.ExecuteSqlCommand("ALTER TABLE [Parcels] NOCHECK CONSTRAINT ALL");
				db.Database.ExecuteSqlCommand("DELETE FROM [Parcels]");
				db.Database.ExecuteSqlCommand("ALTER TABLE [Parcels] WITH CHECK CHECK CONSTRAINT ALL");
				db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT ('[Parcels]', RESEED, {Constants.DbStartIndex})");
			}
			catch (Exception ex)
			{
				logger.LogError("An error occured while cleaning the database", ex);
				throw new DALException("An error occured while cleaning the database", ex);
			}

		}
	}
}
