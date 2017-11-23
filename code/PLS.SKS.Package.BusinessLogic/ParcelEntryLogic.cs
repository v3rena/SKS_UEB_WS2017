using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package;
using PLS.SKS.Package.DataAccess.Sql;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using PLS.SKS.Package.DataAccess.Entities;

namespace PLS.SKS.Package.BusinessLogic
{
    public class ParcelEntryLogic : Interfaces.IParcelEntryLogic
    {
		private DataAccess.Interfaces.IParcelRepository parcelRepo;
		private DataAccess.Interfaces.ITrackingInformationRepository trackingRepo;
		private DataAccess.Interfaces.IHopArrivalRepository hopArrivalRepo;

		public ParcelEntryLogic(IServiceProvider serviceProvider)
		{
			parcelRepo = new DataAccess.Sql.SqlParcelRepository(serviceProvider.GetRequiredService<DataAccess.Sql.DBContext>());
			trackingRepo = new DataAccess.Sql.SqlTrackingInformationRepository(serviceProvider.GetRequiredService<DataAccess.Sql.DBContext>());
			hopArrivalRepo = new DataAccess.Sql.SqlHopArrivalRepository(serviceProvider.GetRequiredService<DataAccess.Sql.DBContext>());
		}

		public string AddParcel(DataAccess.Entities.Parcel parcel)
        {
			parcel.TrackingInformation = GenerateTrackingInformation();
			parcel.TrackingNumber = RandomString(8);
			parcelRepo.Create(parcel);
			return parcel.TrackingNumber;
        }

		private static string RandomString(int length)
		{
			const string pool = "ABCDEFGHIJKLMNOPQRSTUVW0123456789";
			var chars = Enumerable.Range(0, length)
				.Select(x => pool[new Random().Next(0, pool.Length)]);
			return new string(chars.ToArray());
		}

		private TrackingInformation GenerateTrackingInformation()
		{
			var trackInfo = new TrackingInformation(TrackingInformation.StateEnum.InTransportEnum);
			int trackInfoId = trackingRepo.Create(trackInfo);

			//Get truck that is nearest to the given adress and get warehouse hierarchy from there
			var hop1 = new HopArrival { DateTime = DateTime.Now, Code = "WH01", Status = "visited", TrackingInformationId = trackInfoId };
			var hop2 = new HopArrival { DateTime = DateTime.Now.AddDays(1), Code = "WH02", Status = "future", TrackingInformationId = trackInfoId };
			var hop3 = new HopArrival { DateTime = DateTime.Now.AddDays(2), Code = "WH03", Status = "future", TrackingInformationId = trackInfoId };
			var hop4 = new HopArrival { DateTime = DateTime.Now.AddDays(3), Code = "TR01", Status = "future", TrackingInformationId = trackInfoId };

			hopArrivalRepo.Create(hop1);
			hopArrivalRepo.Create(hop2);
			hopArrivalRepo.Create(hop3);
			hopArrivalRepo.Create(hop4);

			trackInfo.futureHops = new List<HopArrival> { hop2, hop3, hop4 };
			trackInfo.visitedHops = new List<HopArrival> { hop1 };

			return trackInfo;
		}
	}
  }
