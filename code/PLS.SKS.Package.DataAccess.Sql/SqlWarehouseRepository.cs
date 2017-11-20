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
			return w.Id;
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Warehouse GetById(int id)
		{
			var warehouse = db.Warehouses.Include(w => w.NextHops).Include(w => w.Trucks)
				.Where(w => w.Id == id).FirstOrDefault();

			while(warehouse.NextHops.Count != 0)
			{
				var id2 = warehouse.NextHops.FirstOrDefault().Id;
				warehouse = db.Warehouses.Include(w => w.NextHops).Include(w => w.Trucks)
				.Where(w => w.Id == id2).FirstOrDefault();
			}

			return db.Warehouses.Include(w => w.NextHops).Include(w => w.Trucks)
				.Where(w => w.Id == id).FirstOrDefault();
		}

		public void Update(Warehouse w)
		{
			var WarehouseToUpdate = db.Warehouses.SingleOrDefault(b => b.Id == w.Id);
			if (WarehouseToUpdate != null)
			{
				WarehouseToUpdate = w;
				db.SaveChanges();
			}
		}
	}
}
