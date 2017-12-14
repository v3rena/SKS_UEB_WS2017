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
using PLS.SKS.Package.BusinessLogic.Validators;

namespace PLS.SKS.Package.BusinessLogic
{
	public class ParcelEntryLogic : Interfaces.IParcelEntryLogic
	{
		private readonly IParcelRepository _parcelRepo;
		private readonly ITrackingInformationRepository _trackingRepo;
		private readonly IHopArrivalRepository _hopArrivalRepo;
		private readonly ITruckRepository _truckRepo;
		private readonly IWarehouseRepository _warehouseRepo;
		private readonly IGeoEncodingAgent _encodingAgent;
		private readonly AutoMapper.IMapper _mapper;
		private readonly ILogger<ParcelEntryLogic> _logger;

		public ParcelEntryLogic(IWarehouseRepository warehouseRepository, ITruckRepository truckRepository, IParcelRepository parcelRepository, ITrackingInformationRepository trackingInformationRepository, IHopArrivalRepository hopArrivalRepository, IGeoEncodingAgent encodingAgent, ILogger<ParcelEntryLogic> logger, AutoMapper.IMapper mapper)
		{
			_parcelRepo = parcelRepository;
			_trackingRepo = trackingInformationRepository;
			_hopArrivalRepo = hopArrivalRepository;
			_truckRepo = truckRepository;
			_warehouseRepo = warehouseRepository;
			_encodingAgent = encodingAgent;
			_logger = logger;
			_mapper = mapper;
		}

		public string AddParcel(IO.Swagger.Models.Parcel serviceParcel)
		{
			try
			{
				if (serviceParcel == null)
				{
					throw new BlException("Received Service Parcel was null");
				}
				var blParcel = _mapper.Map<Entities.Parcel>(serviceParcel);
				if (blParcel != null)
				{
					string validationResults = ValidatePreAddedParcel(blParcel);
					if (validationResults != "")
					{
						_logger.LogError(validationResults);
						throw new BlException("Given parcel is not valid", new ArgumentException("Given Parcel is not valid"));
					}
				}
				var dalParcel = _mapper.Map<DataAccess.Entities.Parcel>(blParcel);
				dalParcel.TrackingInformation = GenerateTrackingInformation(dalParcel);
				dalParcel.TrackingNumber = GenerateTrackingNumber(8);
				_parcelRepo.Create(dalParcel);
				return dalParcel.TrackingNumber;
			}
			catch (Exception ex)
			{
				_logger.LogError("Could not add parcel", ex);
				throw new BlException("Could not add parcel", ex);
			}
		}

		private static string GenerateTrackingNumber(int length)
		{
			const string pool = "ABCDEFGHIJKLMNOPQRSTUVW0123456789";
			var chars = Enumerable.Range(0, length)
				.Select(x => pool[new Random().Next(0, pool.Length)]);
			return new string(chars.ToArray());
		}

		private DataAccess.Entities.TrackingInformation GenerateTrackingInformation(DataAccess.Entities.Parcel parcel)
		{
			//Create new empty TrackingInformation
			var dalTrackInfo = new DataAccess.Entities.TrackingInformation(DataAccess.Entities.TrackingInformation.StateEnum.InTransportEnum);
			var trackInfoId = _trackingRepo.Create(dalTrackInfo);
			dalTrackInfo.FutureHops = new List<DataAccess.Entities.HopArrival>();
			dalTrackInfo.VisitedHops = new List<DataAccess.Entities.HopArrival>();

			//Get destination of parcel
			var blRecipient = _mapper.Map<Entities.Recipient>(parcel.Recipient);
			var saRecipient = _mapper.Map<ServiceAgents.DTOs.Recipient>(blRecipient);
			var saLocation = _encodingAgent.EncodeAddress(saRecipient);
			var blLocation = _mapper.Map<Entities.Location>(saLocation);

			//Select nearest truck
			var truck = SelectNearestTruck(blLocation);
			if (truck == null)
			{
				throw new BlException("The given address is not in the range of service");
			}

			//Get itinerary of parcel and create hop arrival estimations
			var warehouses = GetItinerary(truck);
			var date = DateTime.Now;

			foreach (var wh in warehouses)
			{
				date = date.AddDays((double)wh.Duration);
				var hop = new DataAccess.Entities.HopArrival { DateTime = date, Code = wh.Code, Status = "future", TrackingInformationId = trackInfoId };
				_hopArrivalRepo.Create(hop);
				dalTrackInfo.FutureHops.Add(hop);
			}
			date = date.AddDays((double)truck.Duration);
			var truckHop = new DataAccess.Entities.HopArrival { DateTime = date, Code = truck.Code, Status = "future", TrackingInformationId = trackInfoId };

			_hopArrivalRepo.Create(truckHop);
			dalTrackInfo.FutureHops.Add(truckHop);
			return dalTrackInfo;
		}

		private DataAccess.Entities.Truck SelectNearestTruck(Entities.Location blLocation)
		{
			var trucks = _truckRepo.GetAll();
			var nearestTruck = trucks.FirstOrDefault();
			var smallestDistance = DistanceCalculator.GetDistanceBetweenTwoPoints((double)nearestTruck.Latitude, (double)nearestTruck.Longitude, blLocation.Lat, blLocation.Lng);

			foreach (var truck in trucks)
			{
				var distance = DistanceCalculator.GetDistanceBetweenTwoPoints((double)truck.Latitude, (double)truck.Longitude, blLocation.Lat, blLocation.Lng);
				if (distance < smallestDistance)
				{
					smallestDistance = distance;
					nearestTruck = truck;
				}
			}
			return (decimal)smallestDistance <= nearestTruck.Radius ? nearestTruck : null;
		}

		private List<DataAccess.Entities.Warehouse> GetItinerary(DataAccess.Entities.Truck truck)
		{
			var warehouses = new List<DataAccess.Entities.Warehouse>();
			var warehouse = _warehouseRepo.GetParent(truck);
			warehouses.Add(warehouse);

			while (warehouse != null)
			{
				var parent = _warehouseRepo.GetParent(warehouse);
				if (parent != null)
				{
					warehouses.Add(parent);
				}
				warehouse = parent;
			}
			warehouses.Reverse();
			return warehouses;
		}

		private string ValidatePreAddedParcel(Entities.Parcel blParcel)
		{
			var validator = new PreAddedParcelValidator();
			StringBuilder validationResults = new StringBuilder();
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