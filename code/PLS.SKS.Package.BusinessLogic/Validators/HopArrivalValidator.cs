using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using PLS.SKS.Package.BusinessLogic.Entities;

namespace PLS.SKS.Package.BusinessLogic.Validators
{
	public class HopArrivalValidator : AbstractValidator<HopArrival>
	{
		public HopArrivalValidator()
		{
			RuleFor(parcel => parcel.Code).NotEmpty();
			RuleFor(parcel => parcel.DateTime).NotEmpty().GreaterThanOrEqualTo(DateTime.Now);
		}
	}
}
