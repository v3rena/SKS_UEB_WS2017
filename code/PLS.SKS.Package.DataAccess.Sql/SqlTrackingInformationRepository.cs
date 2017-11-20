using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;
using System.Linq;

namespace PLS.SKS.Package.DataAccess.Sql
{
	public class SqlTrackingInformationRepository : ITrackingInformationRepository
	{
		private readonly DBContext db;

		public SqlTrackingInformationRepository(DBContext context)
		{
			db = context;
		}

		public int Create(TrackingInformation t)
		{
			db.Add(t);
			return t.Id;
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public TrackingInformation GetById(int id)
		{
			return db.TrackingInformations.Find(id);
		}

		public void Update(TrackingInformation t)
		{
			var TrToUpdate = db.TrackingInformations.SingleOrDefault(b => b.Id == t.Id);
			if (TrToUpdate != null)
			{
				TrToUpdate = t;
				db.SaveChanges();
			}
		}
	}
}
