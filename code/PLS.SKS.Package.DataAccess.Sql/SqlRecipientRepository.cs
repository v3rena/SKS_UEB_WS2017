using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using PLS.SKS.Package.DataAccess.Entities;

namespace PLS.SKS.Package.DataAccess.Sql
{
	public class SqlRecipientRepository : IRecipientRepository
	{
		private readonly DBContext db;

		public SqlRecipientRepository(DBContext context)
		{
			db = context;
		}

		public int Create(Recipient r)
		{
			db.Add(r);
			return r.id;
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Recipient GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(Recipient p)
		{
			throw new NotImplementedException();
		}
	}
}
