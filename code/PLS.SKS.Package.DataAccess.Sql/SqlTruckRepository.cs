using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace PLS.SKS.Package.DataAccess.Sql
{
	public class SqlTruckRepository : ITruckRepository
	{
		private readonly DBContext db;
		ILogger<SqlTruckRepository> logger;
		ExceptionHelper exceptionHelper = new ExceptionHelper();

		public SqlTruckRepository(DBContext context, ILogger<SqlTruckRepository> logger)
		{
			db = context;
			this.logger = logger;
		}

		public object WarehouseToUpdate { get; private set; }

		public int Create(Truck t)
		{
			try
			{
				db.Add(t);
				db.SaveChanges();
				return t.Id;
			}
			catch (SqlException ex)
			{
				logger.LogError(exceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not save truck to database", ex);
			}
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Truck GetById(int id)
		{
			try
			{
				return db.Trucks.Find(id);

			}
			catch (SqlException ex)
			{
				logger.LogError(exceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not retrieve truck from database", ex);
			}
		}

		public List<Truck> GetAll()
		{
			try
			{
				return db.Trucks.ToList();
			}
			catch (SqlException ex)
			{
				logger.LogError(exceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not retrieve truck list from database", ex);
			}
		}

		public void Update(Truck t)
		{
			try
			{
				var TruckToUpdate = db.Trucks.SingleOrDefault(b => b.Id == t.Id);
				if (WarehouseToUpdate != null)
				{
					WarehouseToUpdate = t;
					db.SaveChanges();
				}
			}
			catch (SqlException ex)
			{
				logger.LogError(exceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not upadate truck", ex);
			}
		}
	}
}
