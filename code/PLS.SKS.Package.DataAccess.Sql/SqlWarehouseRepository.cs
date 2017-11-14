using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;

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
			return db.Warehouses.Find(id);
		}

		public void Update(Warehouse w)
		{
			throw new NotImplementedException();
		}
	}
}
