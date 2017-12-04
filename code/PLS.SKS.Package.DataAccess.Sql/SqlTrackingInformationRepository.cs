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
				logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not save tracking information to database", ex);
			}
			catch (Exception ex)
			{
				logger.LogError("Could not save tracking information to database", ex);
				throw new DALException("Could not save tracking information to database", ex);
			}
		}

		public void Delete(int id)
		{
			try
			{
				db.Remove(db.TrackingInformations.Where(p => p.Id == id));
			}
			catch (SqlException ex)
			{
				logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not delete tracking information from database", ex);
			}
			catch (Exception ex)
			{
				logger.LogError("Could not delete tracking information", ex);
				throw new DALException("Could not delete tracking information", ex);
			}
		}

		public TrackingInformation GetById(int id)
		{
			try
			{
				return db.TrackingInformations.Find(id);
			}
			catch (SqlException ex)
			{
				logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not retrieve tracking information from database", ex);
			}
			catch (Exception ex)
			{
				logger.LogError("Could not retrieve tracking information from database", ex);
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
				logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not update tracking information", ex);
			}
			catch (Exception ex)
			{
				logger.LogError("Could not update tracking information in database", ex);
				throw new DALException("Could not update tracking information in database", ex);
			}
		}
	}
}
