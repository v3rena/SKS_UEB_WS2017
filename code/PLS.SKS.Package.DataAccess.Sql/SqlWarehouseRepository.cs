using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PLS.SKS.Package.DataAccess.Sql
{
	public class SqlWarehouseRepository : IWarehouseRepository
	{
		private readonly DBContext db;

		public SqlWarehouseRepository(DBContext context)
		{
			db = context;
		}

		public int Create(Warehouse w)
		{
			db.Add(w);
			return w.id;
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Warehouse GetById(int id)
		{
			var warehouse = db.Warehouses.Include(w => w.nextHops).Include(w => w.trucks)
				.Where(w => w.id == id).FirstOrDefault();

			while(warehouse.nextHops.Count != 0)
			{
				var id2 = warehouse.nextHops.FirstOrDefault().id;
				warehouse = db.Warehouses.Include(w => w.nextHops).Include(w => w.trucks)
				.Where(w => w.id == id2).FirstOrDefault();
			}

			return db.Warehouses.Include(w => w.nextHops).Include(w => w.trucks)
				.Where(w => w.id == id).FirstOrDefault();
		}

		public void Update(Warehouse w)
		{
			var WarehouseToUpdate = db.Warehouses.SingleOrDefault(b => b.id == w.id);
			if (WarehouseToUpdate != null)
			{
				WarehouseToUpdate = w;
				db.SaveChanges();
			}
		}
	}
}
