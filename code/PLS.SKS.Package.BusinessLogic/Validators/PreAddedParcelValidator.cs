using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using PLS.SKS.Package.BusinessLogic.Entities;

namespace PLS.SKS.Package.BusinessLogic.Validators
{
	public class PreAddedParcelValidator : AbstractValidator<Parcel>
	{
		public PreAddedParcelValidator()
		{
			RuleFor(parcel => parcel.Weight).NotEmpty().WithMessage("Please specify a weight").GreaterThan(0.0f);
			RuleFor(parcel => parcel.Recipient).SetValidator(new RecipientValidator());
		}
	}
}
