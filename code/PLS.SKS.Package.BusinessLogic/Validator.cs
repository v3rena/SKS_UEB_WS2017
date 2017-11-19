using FluentValidation;
using PLS.SKS.Package.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PLS.SKS.Package.BusinessLogic
{
    public class Validator
    {
		public class HopArrivalValidator : AbstractValidator<HopArrival>
		{
			public HopArrivalValidator()
			{
				RuleFor(parcel => parcel.code).NotEmpty();
				RuleFor(parcel => parcel.dateTime).NotEmpty().GreaterThanOrEqualTo(DateTime.Now);
			}
		}

		public class RecipientValidator : AbstractValidator<Recipient>
		{
			public RecipientValidator()
			{
				RuleFor(recipient => recipient.firstName).NotEmpty().WithMessage("Please specify a first name").Matches(new Regex(@"[A-Z]{1}[A-Za-z -]"));
				RuleFor(recipient => recipient.lastName).NotEmpty().WithMessage("Please specify a last name").Matches(new Regex(@"[A-Z][A-Za-z -]"));
				RuleFor(recipient => recipient.street).NotEmpty().WithMessage("Please specify a street").Matches(new Regex(@"[A-Za-z] [A-Za-z0-9/]"));
				RuleFor(recipient => recipient.postalCode).NotEmpty().WithMessage("Please specify a postal code").Matches(new Regex(@"^A-\d{4}"));
				RuleFor(recipient => recipient.city).NotEmpty().WithMessage("Please specify a city").Matches(new Regex(@"[A-Z][A-Za-z -]"));
			}
		}

		public class ParcelValidator : AbstractValidator<Parcel>
		{
			public ParcelValidator()
			{
				RuleFor(parcel => parcel.Weight).NotEmpty().WithMessage("Please specify a weight").GreaterThan(0.0f);
				RuleFor(parcel => parcel.Recipient).SetValidator(new RecipientValidator());
				RuleFor(parcel=> parcel.TrackingNumber).Matches(new Regex(@"^[A-Z\d]{8}"));
			}
		}

		public class TrackingInformationValidator : AbstractValidator<TrackingInformation>
		{
			public TrackingInformationValidator()
			{
				RuleFor(trackingInformation => trackingInformation.State).NotEmpty().WithMessage("Please specify a state");
				RuleFor(trackingInformation => trackingInformation.visitedHops).SetCollectionValidator(new HopArrivalValidator());
				RuleFor(trackingInformation => trackingInformation.futureHops).SetCollectionValidator(new HopArrivalValidator());
			}
		}

		public class TruckValidator : AbstractValidator<Truck>
		{
			public TruckValidator()
			{
				RuleFor(truck => truck.code).NotEmpty().WithMessage("Please specify a code").Matches(new Regex(@"[A-Z0-9]{4}"));
				RuleFor(truck => truck.numberPlate).NotEmpty().WithMessage("Please specify a numberPlate");
				RuleFor(truck => truck.latitude).NotEmpty().WithMessage("Please specify a latitude");
				RuleFor(truck => truck.longitude).NotEmpty().WithMessage("Please specify a longitude");
				RuleFor(truck => truck.radius).NotEmpty().WithMessage("Please specify a radius").GreaterThanOrEqualTo(0);
			}
		}

		public class WarehouseValidator : AbstractValidator<Warehouse>
		{
			public WarehouseValidator()
			{
				RuleFor(warehouse => warehouse.description).Matches(new Regex(@"[A-Za-z0-9- ]"));
				RuleFor(warehouse => warehouse.code).NotEmpty().WithMessage("Please specify a code").Matches(new Regex(@"[A-Z0-9]{4}"));
				RuleFor(warehouse => warehouse.trucks).SetCollectionValidator(new TruckValidator());
			}
		}
	}
}
