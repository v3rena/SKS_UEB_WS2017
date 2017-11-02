using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;
using System.Linq;

namespace PLS.SKS.Package.DataAccess.Mock
{
    public class MockTruckRepository : Interfaces.ITruckRepository
    {
        public MockTruckRepository()
        {
        }

        public int Create(Truck t)
        {
            t.id = m_id;
            m_id++;
            trucks.Add(t);
            return t.id;
        }

        public void Delete(int id)
        {
            Truck t = trucks.Find(item => item.id == id);
            trucks.Remove(t);
        }

        public Truck GetById(int id)
        {
            return trucks.SingleOrDefault(item => item.id == id);
        }

        public void Update(Truck t)
        {
            Truck t2 = trucks.Find(item => item.id == t.id);
            t = t2;
        }

        private List<Truck> trucks = new List<Truck>();
        private int m_id;
    }
}
