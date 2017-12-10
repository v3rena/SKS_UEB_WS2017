using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using PLS.SKS.Package.DataAccess.Sql.Helpers;

namespace PLS.SKS.Package.DataAccess.Sql
{
	public class SqlWarehouseRepository : IWarehouseRepository
	{
		private readonly DbContext _db;
		private readonly ILogger<SqlWarehouseRepository> _logger;

		public SqlWarehouseRepository(DbContext context, ILogger<SqlWarehouseRepository> logger)
		{
			_db = context;
			_logger = logger;
		}

		public int Create(Warehouse w)
		{
			try
			{
				_db.Add(w);
				_db.SaveChanges();
				return w.Id;
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not save warehouse to database", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not save warehouse to database", ex);
				throw new DalException("Could not save warehouse to database", ex);
			}
		}

		public void Delete(int id)
		{
			try
			{
				_db.Remove(_db.Warehouses.Where(p => p.Id == id));
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not delete warehouse from database", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not delete warehouse", ex);
				throw new DalException("Could not delete warehouse", ex);
			}
		}

		public Warehouse GetById(int id)
		{
			try
			{
				Warehouse warehouse = _db.Warehouses.Include(w => w.NextHops)
				.Include(w => w.Trucks).First(w => w.Id == id);
				SearchWarehouseHierarchy(warehouse);
				return _db.Warehouses.Include(w => w.NextHops)
					.Include(w => w.Trucks).First(w => w.Id == id);
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not retrieve warehouse from database", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not retrieve warehouse from database", ex);
				throw new DalException("Could not retrieve warehouse from database", ex);
			}
		}

		public Warehouse GetParent(Warehouse warehouse)
		{
			try
			{
				List<Warehouse> warehouses = _db.Warehouses.Include(w => w.NextHops).Include(w => w.Trucks).ToList();
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
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not get parent warehouse from database", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not retrieve parent warehouse from database", ex);
				throw new DalException("Could not retrieve parent warehouse from database", ex);
			}
		}

		public Warehouse GetParent(Truck truck)
		{
			try
			{
				List<Warehouse> warehouses = _db.Warehouses.Include(w => w.NextHops).Include(w => w.Trucks).ToList();
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
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not get parent warehouse from database", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not retrieve parent warehouse from database", ex);
				throw new DalException("Could not retrieve parent warehouse from database", ex);
			}
		}

		public Warehouse GetRootWarehouse()
		{
			try
			{
				Warehouse warehouse = _db.Warehouses.Include(w => w.NextHops).Include(w => w.Trucks)
				.First();
				SearchWarehouseHierarchy(warehouse);
				return warehouse;
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not retrieve root warehouse from database", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not retrieve root warehouse from database", ex);
				throw new DalException("Could not retrieve root warehouse from database", ex);
			}
		}

		public void Update(Warehouse w)
		{
			try
			{
				var warehouseToUpdate = _db.Warehouses.Single(b => b.Id == w.Id);
				if (warehouseToUpdate != null)
				{
					warehouseToUpdate = w;
					_db.SaveChanges();
				}
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not update warehouse", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not update warehouse in database", ex);
				throw new DalException("Could not update warehouse in database", ex);
			}
		}

		private void SearchWarehouseHierarchy(Warehouse warehouse)
		{
			if (warehouse.NextHops.Count != 0)
			{
				foreach (var wh in warehouse.NextHops)
				{
					int whId = wh.Id;
					_db.Warehouses.Include(w => w.NextHops)
					.Include(w => w.Trucks).First(w => w.Id == whId);
					SearchWarehouseHierarchy(wh);
				}
			}
		}
	}
}
