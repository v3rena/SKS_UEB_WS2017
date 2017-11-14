using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using static PLS.SKS.Package.BusinessLogic.Validator;
using PLS.SKS.Package;
using PLS.SKS.Package.DataAccess.Sql;
using Microsoft.Extensions.DependencyInjection;

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

		public string addParcel(DataAccess.Entities.Parcel parcel)
        {
			parcel.TrackingNumber = "TN" + inc.ToString();
			inc++;
			int pk = parcelRepo.Create(parcel);
			return parcel.TrackingNumber;
        }
    }
  }
