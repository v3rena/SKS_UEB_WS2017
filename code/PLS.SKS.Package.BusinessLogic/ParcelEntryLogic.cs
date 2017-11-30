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
		IWarehouseRepository warehouseRepo;
		private IGeoEncodingAgent encodingAgent;
		private ILogger<ParcelEntryLogic> logger;
		private AutoMapper.IMapper mapper;

		public ParcelEntryLogic(IWarehouseRepository warehouseRepository, ITruckRepository truckRepository, IParcelRepository parcelRepository, ITrackingInformationRepository trackingInformationRepository, IHopArrivalRepository hopArrivalRepository, IGeoEncodingAgent encodingAgent, ILogger<ParcelEntryLogic> logger, AutoMapper.IMapper mapper)
		{
			parcelRepo = parcelRepository;
			trackingRepo = trackingInformationRepository;
			hopArrivalRepo = hopArrivalRepository;
			truckRepo = truckRepository;
			warehouseRepo = warehouseRepository;
			this.encodingAgent = encodingAgent;
			this.logger = logger;
			this.mapper = mapper;
		}

		public string AddParcel(IO.Swagger.Models.Parcel serviceParcel)
        {
			logger.LogInformation("Calling the AddParcel action");
			try
			{
                if (serviceParcel == null)
                {
                    throw new ArgumentNullException("serviceParcel", "Received Service Parcel was NULL");
                }
				Entities.Parcel blParcel = mapper.Map<Entities.Parcel>(serviceParcel);
				if (blParcel != null)
				{
                    string validationResults = ValidatePreAddedParcel(blParcel);
                    if(validationResults != "")
                    {
                        logger.LogError(validationResults);
                        throw new ArgumentException("Given Parcel is not valid");
                    }
                }
				DataAccess.Entities.Parcel dalParcel = mapper.Map<DataAccess.Entities.Parcel>(blParcel);
				dalParcel.TrackingInformation = GenerateTrackingInformation(dalParcel);
				dalParcel.TrackingNumber = RandomString(8);
				parcelRepo.Create(dalParcel);
				return dalParcel.TrackingNumber;
			}
			catch (Exception ex)
			{
				logger.LogError("Could not add parcel", ex);
				throw new BLException("Could not add parcel", ex);
			}
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
			dalTrackInfo.futureHops = new List<DataAccess.Entities.HopArrival>();
			dalTrackInfo.visitedHops = new List<DataAccess.Entities.HopArrival>();
			Entities.Recipient blRecipient = mapper.Map<Entities.Recipient>(parcel.Recipient);
			ServiceAgents.DTOs.Recipient saRecipient = mapper.Map<ServiceAgents.DTOs.Recipient>(blRecipient);

			var saLocation = encodingAgent.EncodeAddress(saRecipient);

			Entities.Location blLocation = mapper.Map<Entities.Location>(saLocation);

			var truck = SelectNearestTruck(blLocation);
			if (truck == null)
			{
				throw new BLException("The given address is not in the range of service");
			}
			else
			{
				var warehouses = new List<DataAccess.Entities.Warehouse>();
				var warehouse = warehouseRepo.GetParent(truck);
				warehouses.Add(warehouse);

				while(warehouse!=null)
				{
					var parent = warehouseRepo.GetParent(warehouse);
					if (parent!=null)
					{
						warehouses.Add(parent);
					}
					warehouse = parent;
				}

				var date = DateTime.Now.AddDays(1);

				foreach (var wh in warehouses)
				{
					var hop = new DataAccess.Entities.HopArrival { DateTime = date, Code = wh.Code, Status = "future", TrackingInformationId = trackInfoId };
					hopArrivalRepo.Create(hop);
					date = date.AddDays(1);
				}

				var truckHop = new DataAccess.Entities.HopArrival { DateTime = date, Code = truck.Code, Status = "future", TrackingInformationId = trackInfoId };
				hopArrivalRepo.Create(truckHop);

				//Add the hops to DalTrackingInfo

				return dalTrackInfo;
			}
		}

		private bool GetParent(object warehouse)
		{
			throw new NotImplementedException();
		}

		private DataAccess.Entities.Truck SelectNearestTruck(Entities.Location blLocation)
		{
			var trucks = truckRepo.GetAll();
			var nearestTruck = trucks.FirstOrDefault();
			var smallestDistance = DistanceCalculator.GetDistanceBetweenTwoPoints((double)nearestTruck.Latitude, (double)nearestTruck.Longitude, blLocation.Lng, blLocation.Lng);

			foreach (var truck in trucks)
			{
				var distance = DistanceCalculator.GetDistanceBetweenTwoPoints((double)truck.Latitude, (double)truck.Longitude, blLocation.Lng, blLocation.Lng);
				if (distance < smallestDistance)
				{
					smallestDistance = distance;
					nearestTruck = truck;
				}
			}
			if ((decimal)smallestDistance <= nearestTruck.Radius)
			{
				return nearestTruck;
			}
			else
			{
				return null;
			}
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

        private string ValidatePreAddedParcel(Entities.Parcel blParcel)
        {
            StringBuilder validationResults = new StringBuilder();

            Validator.PreAddedParcelValidator validator = new Validator.PreAddedParcelValidator();
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
