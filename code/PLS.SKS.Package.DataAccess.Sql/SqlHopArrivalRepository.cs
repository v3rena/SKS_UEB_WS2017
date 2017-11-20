using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;
using System.Linq;

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
			return h.Id;
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public HopArrival GetById(int id)
		{
			return db.HopArrivals.Find(id);
		}

		public void Update(HopArrival h)
		{
			var HopArrivalToUpdate = db.HopArrivals.SingleOrDefault(b => b.Id == h.Id);
			if (HopArrivalToUpdate != null)
			{
				HopArrivalToUpdate = h;
				db.SaveChanges();
			}
		}
	}
}
