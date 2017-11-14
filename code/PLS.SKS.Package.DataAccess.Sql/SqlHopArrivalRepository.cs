using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;

namespace PLS.SKS.Package.DataAccess.Sql
{
	public class SqlHopArrivalRepository : IHopArrivalRepository
	{
		private readonly DBContext db;

		public SqlHopArrivalRepository(DBContext context)
		{
			db = context;
		}

		public int Create(HopArrival h)
		{
			db.Add(h);
			return h.id;
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public HopArrival GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(HopArrival h)
		{
			throw new NotImplementedException();
		}
	}
}
