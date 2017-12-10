using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using PLS.SKS.Package.DataAccess.Sql.Helpers;

namespace PLS.SKS.Package.DataAccess.Sql
{
	public class SqlParcelRepository : IParcelRepository
	{
		private readonly DbContext _db;
		private readonly ILogger<SqlParcelRepository> _logger;

		public SqlParcelRepository(DbContext context, ILogger<SqlParcelRepository> logger)
		{
			_db = context;
			_logger = logger;
		}

		public int Create(Parcel p)
		{
			try
			{
				_db.Add(p);
				_db.SaveChanges();
				return p.Id;
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not save parcel to database", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not save parcel to database", ex);
				throw new DalException("Could not save parcel to database", ex);
			}
		}

		public void Delete(int id)
		{
			try
			{
				_db.Remove(_db.Parcels.Where(p => p.Id == id));
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not delete parcel from database", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not delete parcel", ex);
				throw new DalException("Could not delete parcel", ex);
			}
		}

		public IEnumerable<Parcel> GetByCode(int code)
		{
			throw new NotImplementedException();
		}

		public Parcel GetById(int id)
		{
			try
			{
				return _db.Parcels.Find(id);
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not retrieve parcel from database", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not retrieve parcel from database", ex);
				throw new DalException("Could not retrieve parcel from database", ex);
			}
		}

		public Parcel GetByTrackingNumber(string trackingNumber)
		{
			try
			{
				return _db.Parcels.Include(p => p.Recipient)
					.Include(p => p.TrackingInformation).First(p => p.TrackingNumber == trackingNumber);
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not retrieve parcel from database", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not retrieve parcel from database", ex);
				throw new DalException("Could not retrieve parcel from database", ex);
			}
		}

		public IEnumerable<Parcel> GetByLengthRanking(int top)
		{
			throw new NotImplementedException();
		}

		public void Update(Parcel p)
		{
			try
			{
				var parcelToUpdate = _db.Parcels.Single(b => b.Id == p.Id);
				if (parcelToUpdate != null)
				{
					parcelToUpdate = p;
					_db.SaveChanges();
				}
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not update parcel", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not update parcel in database", ex);
				throw new DalException("Could not update parcel in database", ex);
			}
		}
	}
}
