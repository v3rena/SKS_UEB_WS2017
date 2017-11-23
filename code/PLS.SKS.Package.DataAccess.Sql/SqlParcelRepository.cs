using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.DataAccess.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PLS.SKS.Package.DataAccess.Sql
{
	public class SqlParcelRepository : IParcelRepository
	{
		private readonly DBContext db;

		public SqlParcelRepository(DBContext context)
		{
			db = context;
		}

		public int Create(Parcel p)
		{
			db.Add(p);
			db.SaveChanges();
			return p.Id;
		}

		public void Delete(int id)
		{
			db.Remove(db.Parcels.Where(p => p.Id == id));
		}

		public IEnumerable<Parcel> GetByCode(int code)
		{
			throw new NotImplementedException();
		}

		public Parcel GetById(int id)
		{
			return db.Parcels.Find(id);
		}

		public Parcel GetByTrackingNumber(string TrackingNumber)
		{
			var parcel = db.Parcels.Include(p =>p.Recipient).Include(p=>p.TrackingInformation)
				.Where(p => p.TrackingNumber == TrackingNumber).FirstOrDefault();

			return parcel;
		}

		public IEnumerable<Parcel> GetByLengthRanking(int top)
		{
			throw new NotImplementedException();
		}

		public void Update(Parcel p)
		{
			var ParcelToUpdate = db.Parcels.SingleOrDefault(b => b.Id == p.Id);
			if (ParcelToUpdate != null)
			{
				ParcelToUpdate = p;
				db.SaveChanges();
			}
		}
	}
}
