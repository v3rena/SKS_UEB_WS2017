using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using PLS.SKS.Package.DataAccess.Entities;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using PLS.SKS.Package.DataAccess.Sql.Helpers;

namespace PLS.SKS.Package.DataAccess.Sql
{
	public class SqlRecipientRepository : IRecipientRepository
	{
		private readonly DbContext _db;
		private readonly ILogger<SqlRecipientRepository> _logger;

		public SqlRecipientRepository(DbContext context, ILogger<SqlRecipientRepository> logger)
		{
			_db = context;
			_logger = logger;
		}

		public int Create(Recipient r)
		{
			try
			{
				_db.Add(r);
				_db.SaveChanges();
				return r.Id;
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not save recipient to database", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not save recipient to database", ex);
				throw new DalException("Could not save recipient to database", ex);
			}
		}

		public void Delete(int id)
		{
			try
			{
				_db.Remove(_db.Recipients.Where(p => p.Id == id));
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not delete recipient from database", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not delete recipient", ex);
				throw new DalException("Could not delete recipient", ex);
			}
		}

		public Recipient GetById(int id)
		{
			try
			{
				return _db.Recipients.Find(id);
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not retrieve recipient from database", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not retrieve recipient from database", ex);
				throw new DalException("Could not retrieve recipient from database", ex);
			}
		}

		public void Update(Recipient p)
		{
			try
			{
				var recipientToUpdate = _db.Recipients.Single(b => b.Id == p.Id);
				if (recipientToUpdate != null)
				{
					recipientToUpdate = p;
					_db.SaveChanges();
				}
			}
			catch (SqlException ex)
			{
				_logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DalException("Could not update recipient", ex);
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not update recipient in database", ex);
				throw new DalException("Could not update recipient in database", ex);
			}
		}
	}
}
