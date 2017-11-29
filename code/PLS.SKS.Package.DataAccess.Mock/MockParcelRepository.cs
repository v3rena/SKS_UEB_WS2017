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
        MockTrackingInformationRepository mockTrackRepo;
		public List<Parcel> parcels = new List<Parcel>();
		int p_id = 0;

		public MockParcelRepository(MockTrackingInformationRepository trRepo)
		{
            mockTrackRepo = trRepo;

            Parcel p1 = new Parcel(25, new Recipient("Christian", "Kern", "Bundesstrasse 12", "A-1010", "Wien"), 1);
            p1.TrackingNumber = "TN000001";
            p1.TrackingInformation = mockTrackRepo.GetById(p1.TrackingInformationId);
            Parcel p2 = new Parcel(1, new Recipient("Ulrike", "Lunacek", "Blumenweg 1", "A-1120", "Wien"), 2);
            p2.TrackingNumber = "TN000002";
            p2.TrackingInformation = mockTrackRepo.GetById(p1.TrackingInformationId);

            Create(p1);
			Create(p2);
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
            try
            {
                var parcel = parcels.Single(item => item.TrackingNumber == trackingNumber);
                return parcel;
            }
            catch
            {
                return null;
            }
		}

		public void Update(Parcel p)
		{
			Parcel p2 = parcels.Find(item => item.Id == p.Id);
			p = p2;
		}
	}
}
