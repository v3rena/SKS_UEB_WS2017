using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using static PLS.SKS.Package.BusinessLogic.Validator;
using PLS.SKS.Package;
using PLS.SKS.Package.DataAccess.Sql;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace PLS.SKS.Package.BusinessLogic
{
    public class ParcelEntryLogic : Interfaces.IParcelEntryLogic
    {
		private DataAccess.Interfaces.IParcelRepository parcelRepo;

		private int inc = 0;

		public ParcelEntryLogic(IServiceProvider serviceProvider)
		{
			parcelRepo = new DataAccess.Sql.SqlParcelRepository(serviceProvider.GetRequiredService<DataAccess.Sql.DBContext>());
		}

		public string AddParcel(DataAccess.Entities.Parcel parcel)
        {
            //Get Last Parcel for increment?
            
			parcel.TrackingNumber = "TN" + inc.ToString();
			inc++;
			int pk = parcelRepo.Create(parcel);
			return parcel.TrackingNumber;
        }

		private static string RandomString(int length)
		{
			const string pool = "ABCDEFGHIJKLMNOPQRSTUVW0123456789";
			var chars = Enumerable.Range(0, length)
				.Select(x => pool[new Random().Next(0, pool.Length)]);
			return new string(chars.ToArray());
		}
	}
  }
