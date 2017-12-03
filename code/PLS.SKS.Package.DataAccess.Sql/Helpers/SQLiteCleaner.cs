using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace PLS.SKS.Package.DataAccess.Sql
{
    public class SQLiteCleaner : DataAccess.Interfaces.IDbCleaner
    {
        private readonly DBContext db;
        ILogger<DbCleaner> logger;

        public SQLiteCleaner(DBContext context, ILogger<DbCleaner> logger)
        {
            db = context;
            this.logger = logger;
        }

        public void CleanDb()
        {
            try
            {
                db.Database.ExecuteSqlCommand("PRAGMA foreign_keys=OFF");
                db.Database.ExecuteSqlCommand("DELETE FROM [Recipients]");
                db.Database.ExecuteSqlCommand("DELETE FROM [Trucks]");
                db.Database.ExecuteSqlCommand("DELETE FROM [Warehouses]");
                db.Database.ExecuteSqlCommand("DELETE FROM [HopArrivals]");
                db.Database.ExecuteSqlCommand("DELETE FROM [TrackingInformations]");
                db.Database.ExecuteSqlCommand("DELETE FROM [Parcels]");
                db.Database.ExecuteSqlCommand("PRAGMA foreign_keys=ON");
            }
            catch (Exception ex)
            {
                logger.LogError("An error occured while cleaning the database", ex);
                throw new DALException("An error occured while cleaning the database", ex);
            }
        }
    }
}
