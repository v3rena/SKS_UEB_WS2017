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
			Create(new Parcel(25, new Recipient("Christian", "Kern", "Bundesstrasse 12", "1010", "Wien"), 1));
			Create(new Parcel(1, new Recipient("Ulrike", "Lunacek", "Blumenweg 1", "1120", "Wien"), 2));
		}

		public int Create(Parcel p)
		{
			p.Id = p_id;
			p_id++;
			parcels.Add(p);
			return p.Id;
		}

		public void Delete(int id)
		{
			Parcel p = parcels.SingleOrDefault(item => item.Id == id);
			parcels.Remove(p);
		}

		public IEnumerable<Parcel> GetByCode(int code)
		{
			throw new NotImplementedException();
		}

		public Parcel GetById(int id)
		{
			return parcels.SingleOrDefault(item => item.Id == id);
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
			Parcel p2 = parcels.Find(item => item.Id == p.Id);
			p = p2;
		}
	}
}
