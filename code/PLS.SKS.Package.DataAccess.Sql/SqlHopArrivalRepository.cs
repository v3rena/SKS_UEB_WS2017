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
		ExceptionHelper exceptionHelper = new ExceptionHelper();

		public SqlHopArrivalRepository(DBContext context, ILogger<SqlHopArrivalRepository> logger)
		{
			db = context;
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
				logger.LogError(exceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not save hopArrival to database", ex);
			}
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public HopArrival GetById(int id)
		{
			try
			{
				return db.HopArrivals.Find(id);
			}
			catch (SqlException ex)
			{
				logger.LogError(exceptionHelper.BuildSqlExceptionMessage(ex));
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
				logger.LogError(exceptionHelper.BuildSqlExceptionMessage(ex));
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
				logger.LogError(exceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not update hopArrival in database", ex);
			}
		}
	}
}
