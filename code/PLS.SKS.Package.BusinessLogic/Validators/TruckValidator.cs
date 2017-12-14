using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FluentValidation;
using PLS.SKS.Package.BusinessLogic.Entities;

namespace PLS.SKS.Package.BusinessLogic.Validators
{
	public class TruckValidator : AbstractValidator<Truck>
	{
		public TruckValidator()
		{
			RuleFor(truck => truck.Code).NotEmpty().WithMessage("Please specify a code").Matches(new Regex(@"[A-Z0-9]{4}"));
			RuleFor(truck => truck.NumberPlate).NotEmpty().WithMessage("Please specify a numberPlate");
			RuleFor(truck => truck.Latitude).NotEmpty().WithMessage("Please specify a latitude");
			RuleFor(truck => truck.Longitude).NotEmpty().WithMessage("Please specify a longitude");
			RuleFor(truck => truck.Radius).NotEmpty().WithMessage("Please specify a radius").GreaterThanOrEqualTo(0);
		}
	}
}
