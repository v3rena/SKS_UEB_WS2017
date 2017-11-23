using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;
using System.Linq;

namespace PLS.SKS.Package.DataAccess.Mock
{
	public class MockHopArrivalRepository : IHopArrivalRepository
	{
		public List<HopArrival> hopArrivals = new List<HopArrival>();
		int h_id = 0;

		public int Create(HopArrival h)
		{
			h.Id = h_id;
			h_id++;
			hopArrivals.Add(h);
			return h.Id;
		}

		public void Delete(int id)
		{
			HopArrival h = hopArrivals.SingleOrDefault(item => item.Id == id);
			hopArrivals.Remove(h);
		}

		public HopArrival GetById(int id)
		{
			return hopArrivals.SingleOrDefault(item => item.Id == id);
		}

        public List<HopArrival> GetByTrackingInformationId(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(HopArrival h)
		{
			HopArrival h2 = hopArrivals.Find(item => item.Id == h.Id);
			h = h2;
		}
	}
}
