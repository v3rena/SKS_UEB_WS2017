using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;
using System.Linq;

namespace PLS.SKS.Package.DataAccess.Sql
{
	public class SqlTruckRepository : ITruckRepository
	{
		private readonly DBContext db;

		public SqlTruckRepository(DBContext context)
		{
			db = context;
		}

		public object WarehouseToUpdate { get; private set; }

		public int Create(Truck t)
		{
			db.Add(t);
			db.SaveChanges();
			return t.Id;
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
			var TruckToUpdate = db.Trucks.SingleOrDefault(b => b.Id == t.Id);
			if (WarehouseToUpdate != null)
			{
				WarehouseToUpdate = t;
				db.SaveChanges();
			}
		}
	}
}
