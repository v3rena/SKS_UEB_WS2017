using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PLS.SKS.Package.BusinessLogic.Entities;
using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package.BusinessLogic.Helpers;
using PLS.SKS.Package.BusinessLogic.Validators;

namespace PLS.SKS.Package.BusinessLogic
{
    public class TrackingLogic : Interfaces.ITrackingLogic
    {
		private readonly IParcelRepository _parcelRepo;
        private readonly IHopArrivalRepository _hopRepo;
		private readonly ILogger<TrackingLogic> _logger;
		private readonly AutoMapper.IMapper _mapper;


		public TrackingLogic(IParcelRepository parcelRepository, IHopArrivalRepository hopRepository, ILogger<TrackingLogic> logger, AutoMapper.IMapper mapper)
		{
			_parcelRepo = parcelRepository;
            _hopRepo = hopRepository;
			_logger = logger;
			_mapper = mapper;
		}

		public IO.Swagger.Models.TrackingInformation TrackParcel(string trackingNumber)
        {
			try
			{
				var dalParcel = _parcelRepo.GetByTrackingNumber(trackingNumber);
				if (dalParcel == null)
				{
					throw new BlException("Parcel not found in Database");
				}

				var hopArr = _hopRepo.GetByTrackingInformationId(dalParcel.TrackingInformationId);

				foreach (var h in hopArr)
				{
					if (h.Status == "visited")
					{
						dalParcel.TrackingInformation.VisitedHops.Add(h);
					}
					else if (h.Status == "future")
					{
						dalParcel.TrackingInformation.FutureHops.Add(h);
					}
				}
	
				var blParcel = _mapper.Map<Parcel>(dalParcel);
				if (blParcel != null)
				{
					_logger.LogError(ValidateParcel(blParcel));
				}
				var info = _mapper.Map<IO.Swagger.Models.TrackingInformation>(blParcel.TrackingInformation);
				return info;
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not find parcel", ex);
				throw new BlException("Could not find parcel", ex);
			}
		}

		public string ValidateParcel(Entities.Parcel blParcel)
		{
			StringBuilder validationResults = new StringBuilder();
			var validator = new ParcelValidator();
			ValidationResult results = validator.Validate(blParcel);
			bool validationSucceeded = results.IsValid;
			IList<ValidationFailure> failures = results.Errors;

			foreach (var failure in failures)
			{
				validationResults.Append(failure);
			}
			return validationResults.ToString();
		}
	}
}
