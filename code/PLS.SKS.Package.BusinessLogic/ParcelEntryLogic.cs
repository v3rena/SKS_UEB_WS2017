using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package;
using PLS.SKS.Package.DataAccess.Sql;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using PLS.SKS.Package.DataAccess.Interfaces;
using PLS.SKS.ServiceAgents.Interfaces;
using Microsoft.Extensions.Logging;
using PLS.SKS.Package.BusinessLogic.Helpers;

namespace PLS.SKS.Package.BusinessLogic
{
    public class ParcelEntryLogic : Interfaces.IParcelEntryLogic
    {
		private IParcelRepository parcelRepo;
		private ITrackingInformationRepository trackingRepo;
		private IHopArrivalRepository hopArrivalRepo;
		private ITruckRepository truckRepo;
		private IGeoEncodingAgent encodingAgent;
		private ILogger<ParcelEntryLogic> logger;
		private AutoMapper.IMapper mapper;

		public ParcelEntryLogic(ITruckRepository truckRepository, IParcelRepository parcelRepository, ITrackingInformationRepository trackingInformationRepository, IHopArrivalRepository hopArrivalRepository, IGeoEncodingAgent encodingAgent, ILogger<ParcelEntryLogic> logger, AutoMapper.IMapper mapper)
		{
			parcelRepo = parcelRepository;
			trackingRepo = trackingInformationRepository;
			hopArrivalRepo = hopArrivalRepository;
			truckRepo = truckRepository;
			this.encodingAgent = encodingAgent;
			this.logger = logger;
			this.mapper = mapper;
		}

		public string AddParcel(IO.Swagger.Models.Parcel serviceParcel)
        {
			logger.LogInformation("Calling the AddParcel action");
			Entities.Parcel blParcel = mapper.Map<Entities.Parcel>(serviceParcel);
			if (blParcel != null)
			{
				logger.LogError(ValidateParcel(blParcel));
			}
			DataAccess.Entities.Parcel dalParcel = mapper.Map<DataAccess.Entities.Parcel>(blParcel);
			dalParcel.TrackingInformation = GenerateTrackingInformation(dalParcel);
			dalParcel.TrackingNumber = RandomString(8);
			parcelRepo.Create(dalParcel);
			return dalParcel.TrackingNumber;
        }

		private static string RandomString(int length)
		{
			const string pool = "ABCDEFGHIJKLMNOPQRSTUVW0123456789";
			var chars = Enumerable.Range(0, length)
				.Select(x => pool[new Random().Next(0, pool.Length)]);
			return new string(chars.ToArray());
		}

		private DataAccess.Entities.TrackingInformation GenerateTrackingInformation(DataAccess.Entities.Parcel parcel)
		{
			var dalTrackInfo = new DataAccess.Entities.TrackingInformation(DataAccess.Entities.TrackingInformation.StateEnum.InTransportEnum);
			int trackInfoId = trackingRepo.Create(dalTrackInfo);

			//Get truck that is nearest to the given adress and get warehouse hierarchy from there
			var hop1 = new DataAccess.Entities.HopArrival { DateTime = DateTime.Now, Code = "WH01", Status = "visited", TrackingInformationId = trackInfoId };
			var hop2 = new DataAccess.Entities.HopArrival { DateTime = DateTime.Now.AddDays(1), Code = "WH02", Status = "future", TrackingInformationId = trackInfoId };
			var hop3 = new DataAccess.Entities.HopArrival { DateTime = DateTime.Now.AddDays(2), Code = "WH03", Status = "future", TrackingInformationId = trackInfoId };
			var hop4 = new DataAccess.Entities.HopArrival { DateTime = DateTime.Now.AddDays(3), Code = "TR01", Status = "future", TrackingInformationId = trackInfoId };

			hopArrivalRepo.Create(hop1);
			hopArrivalRepo.Create(hop2);
			hopArrivalRepo.Create(hop3);
			hopArrivalRepo.Create(hop4);

			dalTrackInfo.futureHops = new List<DataAccess.Entities.HopArrival> { hop2, hop3, hop4 };
			dalTrackInfo.visitedHops = new List<DataAccess.Entities.HopArrival> { hop1 };

			return dalTrackInfo;

			Entities.Recipient blRecipient = mapper.Map<Entities.Recipient>(parcel.Recipient);
			ServiceAgents.DTOs.Recipient saRecipient = mapper.Map<ServiceAgents.DTOs.Recipient>(blRecipient);
			var saLocation = encodingAgent.EncodeAddress(saRecipient);
			Entities.Location blLocation = mapper.Map<Entities.Location>(saLocation);

			//Select truck with shortest distance
			//Retrieve hierarchy of warehouses from that truck
			//Write that info to TrackingInformation of parcel
		}

		private Entities.Truck SelectNearestTruck(Entities.Location blLocation)
		{
			var trucks = truckRepo.GetAll();
			var NearestTruck = trucks.FirstOrDefault();
			var smallestDistance = DistanceCalculator.GetDistanceBetweenTwoPoints((double)NearestTruck.Latitude, (double)NearestTruck.Longitude, blLocation.Lng, blLocation.Lng);

			foreach (var truck in trucks)
			{
				var distance = DistanceCalculator.GetDistanceBetweenTwoPoints((double)truck.Latitude, (double)truck.Longitude, blLocation.Lng, blLocation.Lng);
				if (distance < smallestDistance)
				{
					smallestDistance = distance;
					NearestTruck = truck;
				}
			}
			//Map the truck!
			return new Entities.Truck();
		}

		private string ValidateParcel(Entities.Parcel blParcel)
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
