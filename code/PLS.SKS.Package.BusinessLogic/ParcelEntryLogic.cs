﻿using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using PLS.SKS.Package;
using PLS.SKS.Package.DataAccess.Sql;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using PLS.SKS.Package.DataAccess.Entities;
using PLS.SKS.Package.DataAccess.Interfaces;
using PLS.SKS.ServiceAgents.Interfaces;
using Microsoft.Extensions.Logging;

namespace PLS.SKS.Package.BusinessLogic
{
    public class ParcelEntryLogic : Interfaces.IParcelEntryLogic
    {
		private IParcelRepository parcelRepo;
		private ITrackingInformationRepository trackingRepo;
		private IHopArrivalRepository hopArrivalRepo;
		private IGeoEncodingAgent encodingAgent;
		private ILogger<ParcelEntryLogic> logger;
		private AutoMapper.IMapper mapper;

		public ParcelEntryLogic(IParcelRepository parcelRepository, ITrackingInformationRepository trackingInformationRepository, IHopArrivalRepository hopArrivalRepository, IGeoEncodingAgent encodingAgent, ILogger<ParcelEntryLogic> logger, AutoMapper.IMapper mapper)
		{
			parcelRepo = parcelRepository;
			trackingRepo = trackingInformationRepository;
			hopArrivalRepo = hopArrivalRepository;
			this.encodingAgent = encodingAgent;
			this.logger = logger;
			this.mapper = mapper;
		}

		public string AddParcel(Parcel parcel)
        {
			parcel.TrackingInformation = GenerateTrackingInformation(parcel);
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

		private TrackingInformation GenerateTrackingInformation(Parcel parcel)
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

			Entities.Recipient blRecipient = mapper.Map<Entities.Recipient>(parcel.Recipient);
			ServiceAgents.DTOs.Recipient saRecipient = mapper.Map<ServiceAgents.DTOs.Recipient>(blRecipient);
			var saLocation = encodingAgent.EncodeAddress(saRecipient);
			Entities.Location blLocation = mapper.Map<Entities.Location>(saLocation);

			trackInfo.futureHops = new List<HopArrival> { hop2, hop3, hop4 };
			trackInfo.visitedHops = new List<HopArrival> { hop1 };

			return trackInfo;
		}
	}
  }
