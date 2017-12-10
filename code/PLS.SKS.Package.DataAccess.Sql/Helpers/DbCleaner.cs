using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PLS.SKS.Package.DataAccess.Interfaces;

namespace PLS.SKS.Package.DataAccess.Sql.Helpers
{
	public class DbCleaner : IDbCleaner
    {
		private readonly DbContext _db;
		private readonly ILogger<DbCleaner> _logger;

		public DbCleaner(DbContext context, ILogger<DbCleaner> logger)
		{
			_db = context;
			_logger = logger;
		}

		public virtual void CleanDb()
		{
			try
			{
				_db.Database.ExecuteSqlCommand("ALTER TABLE [Recipients] NOCHECK CONSTRAINT ALL");
				_db.Database.ExecuteSqlCommand("ALTER TABLE [Trucks] NOCHECK CONSTRAINT ALL");
				_db.Database.ExecuteSqlCommand("ALTER TABLE [Warehouses] NOCHECK CONSTRAINT ALL");
				_db.Database.ExecuteSqlCommand("ALTER TABLE [HopArrivals] NOCHECK CONSTRAINT ALL");
				_db.Database.ExecuteSqlCommand("ALTER TABLE [TrackingInformations] NOCHECK CONSTRAINT ALL");
				_db.Database.ExecuteSqlCommand("ALTER TABLE [Parcels] NOCHECK CONSTRAINT ALL");

				_db.Database.ExecuteSqlCommand("DELETE FROM [Recipients]");
				_db.Database.ExecuteSqlCommand("DELETE FROM [Trucks]");
				_db.Database.ExecuteSqlCommand("DELETE FROM [Warehouses]");
				_db.Database.ExecuteSqlCommand("DELETE FROM [HopArrivals]");
				_db.Database.ExecuteSqlCommand("DELETE FROM [TrackingInformations]");
				_db.Database.ExecuteSqlCommand("DELETE FROM [Parcels]");

				_db.Database.ExecuteSqlCommand("ALTER TABLE [Recipients] WITH CHECK CHECK CONSTRAINT ALL");
				_db.Database.ExecuteSqlCommand("ALTER TABLE [Trucks] WITH CHECK CHECK CONSTRAINT ALL");
				_db.Database.ExecuteSqlCommand("ALTER TABLE [Warehouses] WITH CHECK CHECK CONSTRAINT ALL");
				_db.Database.ExecuteSqlCommand("ALTER TABLE [HopArrivals] WITH CHECK CHECK CONSTRAINT ALL");
				_db.Database.ExecuteSqlCommand("ALTER TABLE [TrackingInformations] WITH CHECK CHECK CONSTRAINT ALL");
				_db.Database.ExecuteSqlCommand("ALTER TABLE [Parcels] WITH CHECK CHECK CONSTRAINT ALL");

				_db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT ('[Recipients]', RESEED, 0)");
				_db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT ('[Trucks]', RESEED, 0)");
				_db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT ('[Warehouses]', RESEED, 0)");
				_db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT ('[HopArrivals]', RESEED, 0)");
				_db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT ('[TrackingInformations]', RESEED, 0)");
				_db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT ('[Parcels]', RESEED, 0)");

			}
			catch (Exception ex)
			{
				_logger.LogError("An error occured while cleaning the database", ex);
				throw new DalException("An error occured while cleaning the database", ex);
			}

		}
	}
}
