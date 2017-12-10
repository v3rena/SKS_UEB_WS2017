using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using PLS.SKS.Package.DataAccess.Sql.Helpers;

namespace PLS.SKS.Package.DataAccess.Sql
{
	public class SqlTrackingInformationRepository : ITrackingInformationRepository
	{
		private readonly DbContext _db;
		private readonly ILogger<SqlTrackingInformationRepository> _logger;

		public SqlTrackingInformationRepository(DbContext context, ILogger<SqlTrackingInformationRepository> logger)
		{
			_db = context;
			_logger = logger;
		}

		public int Create(TrackingInformation t)
		{
			try
			{
				_db.Add(t);
				_db.SaveChanges();
				return t.Id;
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not save tracking information to database", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not save tracking information to database", ex);
				throw new DalException("Could not save tracking information to database", ex);
			}
		}

		public void Delete(int id)
		{
			try
			{
				_db.Remove(_db.TrackingInformations.Where(p => p.Id == id));
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not delete tracking information from database", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not delete tracking information", ex);
				throw new DalException("Could not delete tracking information", ex);
			}
		}

		public TrackingInformation GetById(int id)
		{
			try
			{
				return _db.TrackingInformations.Find(id);
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not retrieve tracking information from database", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not retrieve tracking information from database", ex);
				throw new DalException("Could not retrieve tracking information from database", ex);
			}
		}

		public void Update(TrackingInformation t)
		{
			try
			{
				var trToUpdate = _db.TrackingInformations.Single(b => b.Id == t.Id);
				if (trToUpdate != null)
				{
					trToUpdate = t;
					_db.SaveChanges();
				}
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not update tracking information", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not update tracking information in database", ex);
				throw new DalException("Could not update tracking information in database", ex);
			}
		}
	}
}
