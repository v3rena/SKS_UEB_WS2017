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
            Create(new Truck("TR01", "WR-2765", 48.2089816m, 16.373213299999975m, 30m, 0.5m));
            Create(new Truck("TR02", "WR-2766", 48.3059826m, 14.287141199999951m, 30m, 0.5m));
            Create(new Truck("TR03", "WR-2767", 48.85661400000001m, 2.3522219000000177m, 30m, 0.5m));
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
