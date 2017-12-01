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
            t.Id = m_id;
            m_id++;
            trucks.Add(t);
            return t.Id;
        }

        public void Delete(int id)
        {
            Truck t = trucks.Find(item => item.Id == id);
            trucks.Remove(t);
        }

        public Truck GetById(int id)
        {
            return trucks.SingleOrDefault(item => item.Id == id);
        }

        public void Update(Truck t)
        {
            Truck t2 = trucks.Find(item => item.Id == t.Id);
            t = t2;
        }

		public List<Truck> GetAll()
		{
			return trucks;
		}

		private List<Truck> trucks = new List<Truck>();
        private int m_id;
    }
}
