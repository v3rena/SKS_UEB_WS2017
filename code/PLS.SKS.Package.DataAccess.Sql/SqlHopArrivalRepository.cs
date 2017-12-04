using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;
using System.Linq;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace PLS.SKS.Package.DataAccess.Sql
{
	public class SqlHopArrivalRepository : IHopArrivalRepository
	{
		private readonly DBContext db;
		ILogger<SqlHopArrivalRepository> logger;

		public SqlHopArrivalRepository(DBContext context, ILogger<SqlHopArrivalRepository> logger)
		{
			db = context;
			this.logger = logger;
		}

		public int Create(HopArrival h)
		{
			try
			{
				db.Add(h);
				db.SaveChanges();
				return h.Id;
			}
			catch (SqlException ex)
			{
				logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not save hopArrival to database", ex);
			}
			catch (Exception ex)
			{
				logger.LogError("Could not save hopArrival to database", ex);
				throw new DALException("Could not save hopArrival to database", ex);
			}
		}

		public void Delete(int id)
		{
			try
			{
				db.Remove(db.HopArrivals.Where(p => p.Id == id));
			}
			catch (SqlException ex)
			{
				logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not delete hopArrival", ex);
			}
			catch (Exception ex)
			{
				logger.LogError("Could not delete hopArrival", ex);
				throw new DALException("Could not delete hopArrival", ex);
			}
		}

		public HopArrival GetById(int id)
		{
			try
			{
				return db.HopArrivals.Find(id);
			}
			catch (SqlException ex)
			{
				logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not retrieve hopArrival from database", ex);
			}
			catch (Exception ex)
			{
				logger.LogError("Could not retrieve hopArrival from database", ex);
				throw new DALException("Could not retrieve hopArrival from database", ex);
			}
		}

        public List<HopArrival> GetByTrackingInformationId(int id)
        {
			try
			{
				return db.HopArrivals.Where(h => h.TrackingInformationId == id).ToList();
			}
			catch (SqlException ex)
			{
				logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not retrieve hopArrival from database", ex);
			}
			catch (Exception ex)
			{
				logger.LogError("Could not retrieve hopArrival from database", ex);
				throw new DALException("Could not retrieve hopArrival from database", ex);
			}
		}

		public void Update(HopArrival h)
		{
			try
			{
				var HopArrivalToUpdate = db.HopArrivals.SingleOrDefault(b => b.Id == h.Id);
				if (HopArrivalToUpdate != null)
				{
					HopArrivalToUpdate = h;
					db.SaveChanges();
				}
			}
			catch (SqlException ex)
			{
				logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not update hopArrival in database", ex);
			}
			catch (Exception ex)
			{
				logger.LogError("Could not update hopArrival in database", ex);
				throw new DALException("Could not update hopArrival in database", ex);
			}
		}
	}
}
