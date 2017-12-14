using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using PLS.SKS.Package.BusinessLogic.Entities;

namespace PLS.SKS.Package.BusinessLogic.Validators
{
	public class TrackingInformationValidator : AbstractValidator<TrackingInformation>
	{
		public TrackingInformationValidator()
		{
			RuleFor(trackingInformation => trackingInformation.State).NotEmpty().WithMessage("Please specify a state");
			RuleFor(trackingInformation => trackingInformation.VisitedHops).SetCollectionValidator(new HopArrivalValidator());
			RuleFor(trackingInformation => trackingInformation.FutureHops).SetCollectionValidator(new HopArrivalValidator());
		}
	}
}
