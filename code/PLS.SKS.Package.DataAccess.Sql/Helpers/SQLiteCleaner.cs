using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PLS.SKS.Package.DataAccess.Sql.Helpers
{
    public class SqLiteCleaner : DataAccess.Interfaces.IDbCleaner
    {
        private readonly DbContext _db;
        private readonly ILogger<DbCleaner> _logger;

        public SqLiteCleaner(DbContext context, ILogger<DbCleaner> logger)
        {
            _db = context;
            _logger = logger;
        }

        public void CleanDb()
        {
            try
            {
                _db.Database.ExecuteSqlCommand("PRAGMA foreign_keys=OFF");
                _db.Database.ExecuteSqlCommand("DELETE FROM [Recipients]");
                _db.Database.ExecuteSqlCommand("DELETE FROM [Trucks]");
                _db.Database.ExecuteSqlCommand("DELETE FROM [Warehouses]");
                _db.Database.ExecuteSqlCommand("DELETE FROM [HopArrivals]");
                _db.Database.ExecuteSqlCommand("DELETE FROM [TrackingInformations]");
                _db.Database.ExecuteSqlCommand("DELETE FROM [Parcels]");
                _db.Database.ExecuteSqlCommand("PRAGMA foreign_keys=ON");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured while cleaning the database", ex);
                throw new DalException("An error occured while cleaning the database", ex);
            }
        }
    }
}
