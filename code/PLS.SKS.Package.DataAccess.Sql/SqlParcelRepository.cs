using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace PLS.SKS.Package.DataAccess.Sql
{
	public class SqlParcelRepository : IParcelRepository
	{
		private readonly DBContext db;
		ILogger<SqlParcelRepository> logger;
		ExceptionHelper exceptionHelper = new ExceptionHelper();

		public SqlParcelRepository(DBContext context, ILogger<SqlParcelRepository> logger)
		{
			db = context;
			this.logger = logger;
		}

		public int Create(Parcel p)
		{
			try
			{
				db.Add(p);
				db.SaveChanges();
				return p.Id;
			}
			catch (SqlException ex)
			{
				logger.LogError(exceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not save parcel to database", ex);
			}
		}

		public void Delete(int id)
		{
			try
			{
				db.Remove(db.Parcels.Where(p => p.Id == id));

			}
			catch (SqlException ex)
			{
				logger.LogError(exceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not delete parcel from database", ex);
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
				return db.Parcels.Find(id);
			}
			catch (SqlException ex)
			{
				logger.LogError(exceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not retrieve parcel from database", ex);
			}
		}

		public Parcel GetByTrackingNumber(string TrackingNumber)
		{
			try
			{
				return db.Parcels.Include(p => p.Recipient).Include(p => p.TrackingInformation)
				.Where(p => p.TrackingNumber == TrackingNumber).FirstOrDefault();
			}
			catch (SqlException ex)
			{
				logger.LogError(exceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not retrieve parcel from database", ex);
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
				var ParcelToUpdate = db.Parcels.SingleOrDefault(b => b.Id == p.Id);
				if (ParcelToUpdate != null)
				{
					ParcelToUpdate = p;
					db.SaveChanges();
				}
			}
			catch (SqlException ex)
			{
				logger.LogError(exceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not update parcel", ex);
			}
		}
	}
}
