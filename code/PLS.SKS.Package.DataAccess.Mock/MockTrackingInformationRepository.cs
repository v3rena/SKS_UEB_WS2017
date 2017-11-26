using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;
using System.Linq;

namespace PLS.SKS.Package.DataAccess.Mock
{
	public class MockTrackingInformationRepository : ITrackingInformationRepository
	{
		public List<TrackingInformation> trackingInformations = new List<TrackingInformation>();
		int t_id = 2;

        public MockTrackingInformationRepository()
        {
            var trackInfos = new TrackingInformation[]
            {
                new TrackingInformation{Id = 1, State=TrackingInformation.StateEnum.DeliveredEnum },
                new TrackingInformation{Id = 2, State=TrackingInformation.StateEnum.InTransportEnum }
            };
            trackingInformations = trackInfos.ToList();
        }

		public int Create(TrackingInformation t)
		{
            t_id++;
            t.Id = t_id;
			trackingInformations.Add(t);
			return t.Id;
		}

		public void Delete(int id)
		{
			TrackingInformation t = trackingInformations.SingleOrDefault(item => item.Id == id);
			trackingInformations.Remove(t);
		}

		public TrackingInformation GetById(int id)
		{
			return trackingInformations.SingleOrDefault(item => item.Id == id);
		}

		public void Update(TrackingInformation t)
		{
			TrackingInformation t2 = trackingInformations.Find(item => item.Id == t.Id);
			t = t2;
		}
	}
}
