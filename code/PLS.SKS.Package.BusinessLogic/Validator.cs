using FluentValidation;
using PLS.SKS.Package.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
				RuleFor(recipient => recipient.firstName).NotEmpty().WithMessage("Please specify a first name");
				RuleFor(recipient => recipient.lastName).NotEmpty().WithMessage("Please specify a last name");
				RuleFor(recipient => recipient.street).NotEmpty().WithMessage("Please specify a street");
				RuleFor(recipient => recipient.postalCode).NotEmpty().WithMessage("Please specify a postal code");
				RuleFor(recipient => recipient.city).NotEmpty().WithMessage("Please specify a city");
			}
		}

		public class ParcelValidator : AbstractValidator<Parcel>
		{
			public ParcelValidator()
			{
				RuleFor(parcel => parcel.Weight).NotEmpty().WithMessage("Please specify a weight").GreaterThan(0);
				RuleFor(parcel => parcel.Recipient).SetValidator(new RecipientValidator());
			}
		}

		public class TrackingInformationValidator : AbstractValidator<TrackingInformation>
		{
			public TrackingInformationValidator()
			{
				RuleFor(trackingInformation => trackingInformation.state).NotEmpty().WithMessage("Please specify a state");
				RuleFor(trackingInformation => trackingInformation.visitedHops).SetCollectionValidator(new HopArrivalValidator());
				RuleFor(trackingInformation => trackingInformation.futureHops).SetCollectionValidator(new HopArrivalValidator());
			}
		}

		public class TruckValidator : AbstractValidator<Truck>
		{
			public TruckValidator()
			{
				RuleFor(truck => truck.code).NotEmpty().WithMessage("Please specify a code");
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
				RuleFor(warehouse => warehouse.code).NotEmpty().WithMessage("Please specify a code");
				RuleFor(warehouse => warehouse.trucks).SetCollectionValidator(new TruckValidator());
			}
		}
	}
}
