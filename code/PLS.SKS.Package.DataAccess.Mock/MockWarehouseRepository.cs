using System;
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
            //Create(new Entities.Warehouse("Bez1_1", "descr", 1.0m, new List<Entities.Warehouse>(), new List<Entities.Truck>()));
            //Create(new Entities.Warehouse("Reg1_1", "descr", 1.0m, new List<Entities.Warehouse>(), new List<Entities.Truck>()));
            //Create(new Entities.Warehouse("Root", "descr", 1.0m, new List<Entities.Warehouse>(), new List<Entities.Truck>()));
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

        private List<Entities.Warehouse> warehouses = new List<Entities.Warehouse>();
        private int m_id = 0;
    }
}
