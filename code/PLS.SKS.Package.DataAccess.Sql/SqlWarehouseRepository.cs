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
			db.SaveChanges();
			return w.Id;
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Warehouse GetById(int id)
		{
			Warehouse warehouse = db.Warehouses.Include(w => w.NextHops).Include(w => w.Trucks)
			.Where(w => w.Id == id).FirstOrDefault();

			SearchWarehouseHierarchy(warehouse);

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

		private void SearchWarehouseHierarchy(Warehouse warehouse)
		{
			if (warehouse.NextHops.Count != 0)
			{
				foreach (var wh in warehouse.NextHops)
				{
					int whId = wh.Id;
					db.Warehouses.Include(w => w.NextHops).Include(w => w.Trucks)
					.Where(w => w.Id == whId).FirstOrDefault();
					SearchWarehouseHierarchy(wh);
				}
			}
		}
	}
}
