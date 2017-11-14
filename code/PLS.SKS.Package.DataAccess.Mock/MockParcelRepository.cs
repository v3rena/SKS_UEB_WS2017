using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;
using System.Linq;

namespace PLS.SKS.Package.DataAccess.Mock
{
	public class MockParcelRepository : IParcelRepository
	{
		public List<Parcel> parcels = new List<Parcel>();
		int p_id = 0;

		public MockParcelRepository()
		{
			//Create(new Parcel(25, new Recipient("Christian", "Kern", "Bundesstrasse 12", "1010", "Wien")));
			//Create(new Parcel(1, new Recipient("Ulrike", "Lunacek", "Blumenweg 1", "1120", "Wien")));
		}

		public int Create(Parcel p)
		{
			p.id = p_id;
			p_id++;
			parcels.Add(p);
			return p.id;
		}

		public void Delete(int id)
		{
			Parcel p = parcels.SingleOrDefault(item => item.id == id);
			parcels.Remove(p);
		}

		public IEnumerable<Parcel> GetByCode(int code)
		{
			throw new NotImplementedException();
		}

		public Parcel GetById(int id)
		{
			return parcels.SingleOrDefault(item => item.id == id);
		}

		public IEnumerable<Parcel> GetByLengthRanking(int top)
		{
			throw new NotImplementedException();
		}

		public Parcel GetByTrackingNumber(string trackingNumber)
		{
			throw new NotImplementedException();
		}

		public void Update(Parcel p)
		{
			Parcel p2 = parcels.Find(item => item.id == p.id);
			p = p2;
		}
	}
}
