using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;

namespace PLS.SKS.Package.DataAccess.Sql
{
	public class SqlParcelRepository : IParcelRepository
	{
		public int Create(Parcel p)
		{
			throw new NotImplementedException();
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
