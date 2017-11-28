using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PLS.SKS.Package.BusinessLogic.Entities;
using PLS.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLS.SKS.Package.BusinessLogic
{
    public class TrackingLogic : Interfaces.ITrackingLogic
    {
		private IParcelRepository parcelRepo;
        private IHopArrivalRepository hopRepo;
        private ITrackingInformationRepository trackRepo;
		private ILogger<TrackingLogic> logger;
		private AutoMapper.IMapper mapper;


		public TrackingLogic(IParcelRepository parcelRepository, IHopArrivalRepository hopRepository, ITrackingInformationRepository trackRepository, ILogger<TrackingLogic> logger, AutoMapper.IMapper mapper)
		{
			parcelRepo = parcelRepository;
            hopRepo = hopRepository;
            trackRepo = trackRepository;
			this.logger = logger;
			this.mapper = mapper;
		}

		public IO.Swagger.Models.TrackingInformation TrackParcel(string trackingNumber)
        {

			logger.LogInformation("Calling the TrackParcel action");

			DataAccess.Entities.Parcel dalParcel = parcelRepo.GetByTrackingNumber(trackingNumber);

            //get HopArrivals with "TrackingInformationID"
            List<DataAccess.Entities.HopArrival> hopArr = hopRepo.GetByTrackingInformationId(dalParcel.TrackingInformationId);
            //fill visitedHops and futureHops lists
            foreach(var h in hopArr)
            {
                if(h.Status == "visited")
                {
                    dalParcel.TrackingInformation.visitedHops.Add(h);
                }
                else if(h.Status == "future")
                {
                    dalParcel.TrackingInformation.futureHops.Add(h);
                }
            }

			Entities.Parcel blParcel = mapper.Map<Entities.Parcel>(dalParcel);
			if (blParcel != null)
			{
				logger.LogError(ValidateParcel(blParcel));

			}
			IO.Swagger.Models.TrackingInformation info = mapper.Map<IO.Swagger.Models.TrackingInformation>(blParcel.TrackingInformation);
			return info;
		}

		public string ValidateParcel(Entities.Parcel blParcel)
		{
			StringBuilder validationResults = new StringBuilder();

			Validator.ParcelValidator validator = new Validator.ParcelValidator();
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
