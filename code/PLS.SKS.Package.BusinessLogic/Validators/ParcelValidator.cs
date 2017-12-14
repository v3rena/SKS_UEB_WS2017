using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FluentValidation;
using PLS.SKS.Package.BusinessLogic.Entities;

namespace PLS.SKS.Package.BusinessLogic.Validators
{
	public class ParcelValidator : AbstractValidator<Parcel>
	{
		public ParcelValidator()
		{
			RuleFor(parcel => parcel.Weight).NotEmpty().WithMessage("Please specify a weight").GreaterThan(0.0f);
			RuleFor(parcel => parcel.Recipient).SetValidator(new RecipientValidator());
			RuleFor(parcel => parcel.TrackingNumber).Matches(new Regex(@"^[A-Z\d]{8}"));
		}
	}
}
