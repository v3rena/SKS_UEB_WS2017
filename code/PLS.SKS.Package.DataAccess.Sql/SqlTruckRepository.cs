using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;

namespace PLS.SKS.Package.DataAccess.Sql
{
	public class SqlTruckRepository : ITruckRepository
	{
		private readonly DBContext db;

		public SqlTruckRepository(DBContext context)
		{
			db = context;
		}

		public int Create(Truck t)
		{
			db.Add(t);
			return t.id;
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Truck GetById(int id)
		{
			return db.Trucks.Find(id);
		}

		public void Update(Truck t)
		{
			throw new NotImplementedException();
		}
	}
}
