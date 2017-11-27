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
	public class SqlTrackingInformationRepository : ITrackingInformationRepository
	{
		private readonly DBContext db;
		ILogger<SqlTrackingInformationRepository> logger;
		ExceptionHelper exceptionHelper = new ExceptionHelper();

		public SqlTrackingInformationRepository(DBContext context, ILogger<SqlTrackingInformationRepository> logger)
		{
			db = context;
			this.logger = logger;
		}

		public int Create(TrackingInformation t)
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
				throw new DALException("Could not save tracking information to database", ex);
			}
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public TrackingInformation GetById(int id)
		{
			try
			{
				return db.TrackingInformations.Find(id);
			}
			catch (SqlException ex)
			{
				logger.LogError(exceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not retrieve tracking information from database", ex);
			}
		}

		public void Update(TrackingInformation t)
		{
			try
			{
				var TrToUpdate = db.TrackingInformations.SingleOrDefault(b => b.Id == t.Id);
				if (TrToUpdate != null)
				{
					TrToUpdate = t;
					db.SaveChanges();
				}
			}
			catch (SqlException ex)
			{
				logger.LogError(exceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not update tracking information", ex);
			}
		}
	}
}
