using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;
using System.Linq;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using PLS.SKS.Package.DataAccess.Sql.Helpers;

namespace PLS.SKS.Package.DataAccess.Sql
{
	public class SqlHopArrivalRepository : IHopArrivalRepository
	{
		private readonly DbContext _db;
		private readonly ILogger<SqlHopArrivalRepository> _logger;

		public SqlHopArrivalRepository(DbContext context, ILogger<SqlHopArrivalRepository> logger)
		{
			_db = context;
			_logger = logger;
		}

		public int Create(HopArrival h)
		{
			try
			{
				_db.Add(h);
				_db.SaveChanges();
				return h.Id;
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not save hopArrival to database", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not save hopArrival to database", ex);
				throw new DalException("Could not save hopArrival to database", ex);
			}
		}

		public void Delete(int id)
		{
			try
			{
				_db.Remove(_db.HopArrivals.Where(p => p.Id == id));
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not delete hopArrival", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not delete hopArrival", ex);
				throw new DalException("Could not delete hopArrival", ex);
			}
		}

		public HopArrival GetById(int id)
		{
			try
			{
				return _db.HopArrivals.Find(id);
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not retrieve hopArrival from database", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not retrieve hopArrival from database", ex);
				throw new DalException("Could not retrieve hopArrival from database", ex);
			}
		}

        public List<HopArrival> GetByTrackingInformationId(int id)
        {
			try
			{
				return _db.HopArrivals.Where(h => h.TrackingInformationId == id).ToList();
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not retrieve hopArrival from database", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not retrieve hopArrival from database", ex);
				throw new DalException("Could not retrieve hopArrival from database", ex);
			}
		}

		public void Update(HopArrival h)
		{
			try
			{
				var hopArrivalToUpdate = _db.HopArrivals.Single(b => b.Id == h.Id);
				if (hopArrivalToUpdate != null)
				{
					hopArrivalToUpdate = h;
					_db.SaveChanges();
				}
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not update hopArrival in database", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not update hopArrival in database", ex);
				throw new DalException("Could not update hopArrival in database", ex);
			}
		}
	}
}
