using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;
using System.Linq;

namespace PLS.SKS.Package.DataAccess.Sql
{
	public class SqlParcelRepository : IParcelRepository
	{
		private readonly DBContext db;

		//public SqlParcelRepository() { }

		public SqlParcelRepository(DBContext context)
		{
			db = context;
		}

		public int Create(Parcel p)
		{
			db.Add(p);
			return p.id;
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Parcel> GetByCode(int code)
		{
			throw new NotImplementedException();
		}

		public Parcel GetById(int id)
		{
			return db.Parcels.Find(id);
		}

		public IEnumerable<Parcel> GetByLengthRanking(int top)
		{
			throw new NotImplementedException();
		}

		public void Update(Parcel p)
		{
			throw new NotImplementedException();
		}
	}
}
