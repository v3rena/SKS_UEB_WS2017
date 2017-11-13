using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;

namespace PLS.SKS.Package.DataAccess.Sql
{
	public class SqlParcelRepository : IParcelRepository
	{
		private readonly ParcelLogisticsDBContext _context;

		public SqlParcelRepository() { }

		public SqlParcelRepository(ParcelLogisticsDBContext context)
		{
			_context = context;
		}

		public int Create(Parcel p)
		{
			_context.Add(p);
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
			throw new NotImplementedException();
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
