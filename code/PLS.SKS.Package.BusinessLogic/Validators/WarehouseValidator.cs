using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FluentValidation;
using PLS.SKS.Package.BusinessLogic.Entities;

namespace PLS.SKS.Package.BusinessLogic.Validators
{
	public class WarehouseValidator : AbstractValidator<Warehouse>
	{
		public WarehouseValidator()
		{
			RuleFor(warehouse => warehouse.Description).Matches(new Regex(@"[A-Za-z0-9- ]"));
			RuleFor(warehouse => warehouse.Code).NotEmpty().WithMessage("Please specify a code").Matches(new Regex(@"[A-Z0-9]{4}"));
			RuleFor(warehouse => warehouse.Trucks).SetCollectionValidator(new TruckValidator());
		}
	}
}
