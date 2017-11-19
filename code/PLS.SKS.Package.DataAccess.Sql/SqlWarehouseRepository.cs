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
			var w01 = db.Warehouses.Include(w => w.nextHops).Include(w => w.trucks)
				.Where(w => w.id == id).FirstOrDefault();
			var id2 = w01.nextHops.FirstOrDefault().id;
			var w02 = db.Warehouses.Include(w => w.nextHops).Include(w => w.trucks)
				.Where(w => w.id == id2).FirstOrDefault();
			var id3 = w02.nextHops.FirstOrDefault().id;
			var w03 = db.Warehouses.Include(w => w.nextHops).Include(w => w.trucks)
				.Where(w => w.id == id3).FirstOrDefault();

			//w02.nextHops.Add(w03);

			return w01;
		}

		public void Update(Warehouse w)
		{
			throw new NotImplementedException();
		}
	}
}
