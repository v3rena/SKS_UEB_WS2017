using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PLS.SKS.Package.DataAccess.Sql;

namespace PLS.SKS.Package.Services
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void SetupDB(IServiceCollection services)
        {
            //Add Database Context
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "ParcelLogistics.db" };
            var sqliteConnection = new SqliteConnection(connectionStringBuilder.ToString());
            var test = sqliteConnection.ConnectionString;
            sqliteConnection.Open();

            services.AddDbContext<DataAccess.Sql.DBContext>(options =>
            options.UseSqlite(sqliteConnection));
        }

        public override void EnsureDatabaseCreated(DBContext dbContext)
        {
            dbContext.Database.OpenConnection();
            //var created = dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();
            DBInitializer.Initialize(dbContext);
        }
    }
}
