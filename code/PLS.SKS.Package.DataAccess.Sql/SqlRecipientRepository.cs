using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using PLS.SKS.Package.DataAccess.Entities;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace PLS.SKS.Package.DataAccess.Sql
{
	public class SqlRecipientRepository : IRecipientRepository
	{
		private readonly DBContext db;
		ILogger<SqlRecipientRepository> logger;

		public SqlRecipientRepository(DBContext context, ILogger<SqlRecipientRepository> logger)
		{
			db = context;
			this.logger = logger;
		}

		public int Create(Recipient r)
		{
			try
			{
				db.Add(r);
				db.SaveChanges();
				return r.Id;
			}
			catch (SqlException ex)
			{
				logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not save recipient to database", ex);
			}
			catch (Exception ex)
			{
				logger.LogError("Could not save recipient to database", ex);
				throw new DALException("Could not save recipient to database", ex);
			}
		}

		public void Delete(int id)
		{
			try
			{
				db.Remove(db.Recipients.Where(p => p.Id == id));
			}
			catch (SqlException ex)
			{
				logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not delete recipient from database", ex);
			}
			catch (Exception ex)
			{
				logger.LogError("Could not delete recipient", ex);
				throw new DALException("Could not delete recipient", ex);
			}
		}

		public Recipient GetById(int id)
		{
			try
			{
				return db.Recipients.Find(id);
			}
			catch (SqlException ex)
			{
				logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not retrieve recipient from database", ex);
			}
			catch (Exception ex)
			{
				logger.LogError("Could not retrieve recipient from database", ex);
				throw new DALException("Could not retrieve recipient from database", ex);
			}
		}

		public void Update(Recipient p)
		{
			try
			{
				var RecipientToUpdate = db.Recipients.SingleOrDefault(b => b.Id == p.Id);
				if (RecipientToUpdate != null)
				{
					RecipientToUpdate = p;
					db.SaveChanges();
				}
			}
			catch (SqlException ex)
			{
				logger.LogError(ExceptionHelper.BuildSqlExceptionMessage(ex));
				throw new DALException("Could not update recipient", ex);
			}
			catch (Exception ex)
			{
				logger.LogError("Could not update recipient in database", ex);
				throw new DALException("Could not update recipient in database", ex);
			}
		}
	}
}
