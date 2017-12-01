﻿using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;
using System.Linq;

namespace PLS.SKS.Package.DataAccess.Mock
{
    public class MockWarehouseRepository : Interfaces.IWarehouseRepository
    {
        public MockWarehouseRepository()
        {
            var wh3 = new Warehouse("Bez1_1", "descr", 1.0m, new List<Warehouse>(), new List<Truck>());
            wh3.Id = 3;
            var wh2List = new List<Warehouse>();
            wh2List.Add(wh3);
            var wh2 = new Warehouse("Reg1_1", "descr", 1.0m, wh2List, new List<Truck>());
            wh2.Id = 2;
            var wh1List = new List<Warehouse>();
            wh1List.Add(wh2);
            var wh1 = new Warehouse("Root", "descr", 1.0m, wh1List, new List<Truck>());
            wh1.Id = 1;

            warehouses.Add(wh1);
            warehouses.Add(wh2);
            warehouses.Add(wh3);
            m_id = 4;
        }
        public MockWarehouseRepository(bool b)
        {
            m_id = 1;
        }

        public int Create(Warehouse w)
        {
            w.Id = m_id;
            m_id++;
            warehouses.Add(w);
            return 0;
        }

        public void Update(Warehouse w)
        {
            Warehouse w2 = warehouses.Find(item => item.Id == w.Id);
            w = w2;
        }

        public void Delete(int id)
        {
            Warehouse w = warehouses.SingleOrDefault(item => item.Id == id);
            warehouses.Remove(w);
        }

        public Warehouse GetById(int id)
        {
            return warehouses.SingleOrDefault(item => item.Id == id);
        }

		public Warehouse GetParent(Truck truck)
		{
			throw new NotImplementedException();
		}

		public Warehouse GetParent(Warehouse warehouse)
		{
			throw new NotImplementedException();
		}

		private List<Entities.Warehouse> warehouses = new List<Entities.Warehouse>();
        private int m_id;
    }
}
