using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace PLS.SKS.Package.DataAccess.Sql
{
	public class SqlWarehouseRepository : IWarehouseRepository
	{
		private readonly DBContext db;
		ILogger<SqlWarehouseRepository> logger;

		public SqlWarehouseRepository(DBContext context, ILogger<SqlWarehouseRepository> logger)
		{
			db = context;
			this.logger = logger;
		}

		public int Create(Warehouse w)
		{
			try
			{
				db.Add(w);
				db.SaveChanges();
				return w.Id;
			}
			catch (SqlException ex)
			{
				logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not save warehouse to database", ex);
			}
			catch (Exception ex)
			{
				logger.LogError("Could not save warehouse to database", ex);
				throw new DALException("Could not save warehouse to database", ex);
			}
		}

		public void Delete(int id)
		{
			try
			{
				db.Remove(db.Warehouses.Where(p => p.Id == id));
			}
			catch (SqlException ex)
			{
				logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not delete warehouse from database", ex);
			}
			catch (Exception ex)
			{
				logger.LogError("Could not delete warehouse", ex);
				throw new DALException("Could not delete warehouse", ex);
			}
		}

		public Warehouse GetById(int id)
		{
			try
			{
				Warehouse warehouse = db.Warehouses.Include(w => w.NextHops).Include(w => w.Trucks)
				.Where(w => w.Id == id).First();
				SearchWarehouseHierarchy(warehouse);
				return db.Warehouses.Include(w => w.NextHops).Include(w => w.Trucks)
					.Where(w => w.Id == id).First();
			}
			catch (SqlException ex)
			{
				logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not retrieve warehouse from database", ex);
			}
			catch (Exception ex)
			{
				logger.LogError("Could not retrieve warehouse from database", ex);
				throw new DALException("Could not retrieve warehouse from database", ex);
			}
		}

		public Warehouse GetParent(Warehouse warehouse)
		{
			try
			{
				List<Warehouse> warehouses = db.Warehouses.Include(w => w.NextHops).Include(w => w.Trucks).ToList();
				foreach (var wh in warehouses)
				{
					if (wh.NextHops.Contains(warehouse))
					{
						return wh;
					}
				}
				return null;
			}
			catch (SqlException ex)
			{
				logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not get parent warehouse from database", ex);
			}
			catch (Exception ex)
			{
				logger.LogError("Could not retrieve parent warehouse from database", ex);
				throw new DALException("Could not retrieve parent warehouse from database", ex);
			}
		}

		public Warehouse GetParent(Truck truck)
		{
			try
			{
				List<Warehouse> warehouses = db.Warehouses.Include(w => w.NextHops).Include(w => w.Trucks).ToList();
				foreach (var wh in warehouses)
				{
					if (wh.Trucks.Contains(truck))
					{
						return wh;
					}
				}
				return null;
			}
			catch (SqlException ex)
			{
				logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not get parent warehouse from database", ex);
			}
			catch (Exception ex)
			{
				logger.LogError("Could not retrieve parent warehouse from database", ex);
				throw new DALException("Could not retrieve parent warehouse from database", ex);
			}
		}

		public void Update(Warehouse w)
		{
			try
			{
				var WarehouseToUpdate = db.Warehouses.Single(b => b.Id == w.Id);
				if (WarehouseToUpdate != null)
				{
					WarehouseToUpdate = w;
					db.SaveChanges();
				}
			}
			catch (SqlException ex)
			{
				logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not update warehouse", ex);
			}
			catch (Exception ex)
			{
				logger.LogError("Could not update warehouse in database", ex);
				throw new DALException("Could not update warehouse in database", ex);
			}
		}

		private void SearchWarehouseHierarchy(Warehouse warehouse)
		{
			if (warehouse.NextHops.Count != 0)
			{
				foreach (var wh in warehouse.NextHops)
				{
					int whId = wh.Id;
					db.Warehouses.Include(w => w.NextHops).Include(w => w.Trucks)
					.Where(w => w.Id == whId).First();
					SearchWarehouseHierarchy(wh);
				}
			}
		}
	}
}
