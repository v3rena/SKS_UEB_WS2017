using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;
using System.Linq;

namespace PLS.SKS.Package.DataAccess.Mock
{
	class MockHopArrivalRepository : IHopArrivalRepository
	{
		public List<HopArrival> hopArrivals = new List<HopArrival>();
		int h_id = 0;

		public int Create(HopArrival h)
		{
			h.id = h_id;
			h_id++;
			hopArrivals.Add(h);
			return h.id;
		}

		public void Delete(int id)
		{
			HopArrival h = hopArrivals.SingleOrDefault(item => item.id == id);
			hopArrivals.Remove(h);
		}

		public HopArrival GetById(int id)
		{
			return hopArrivals.SingleOrDefault(item => item.id == id);
		}

		public void Update(HopArrival h)
		{
			HopArrival h2 = hopArrivals.Find(item => item.id == h.id);
			h = h2;
		}
	}
}
