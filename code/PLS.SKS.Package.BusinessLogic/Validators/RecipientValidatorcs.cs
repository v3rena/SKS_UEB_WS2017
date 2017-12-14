using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FluentValidation;
using PLS.SKS.Package.BusinessLogic.Entities;

namespace PLS.SKS.Package.BusinessLogic.Validators
{
	public class RecipientValidator : AbstractValidator<Recipient>
	{
		public RecipientValidator()
		{
			RuleFor(recipient => recipient.FirstName).NotEmpty().WithMessage("Please specify a first name").Matches(new Regex(@"[A-Z]{1}[A-Za-z -]"));
			RuleFor(recipient => recipient.LastName).NotEmpty().WithMessage("Please specify a last name").Matches(new Regex(@"[A-Z][A-Za-z -]"));
			RuleFor(recipient => recipient.Street).NotEmpty().WithMessage("Please specify a street").Matches(new Regex(@"[A-Za-z] [A-Za-z0-9/]"));
			RuleFor(recipient => recipient.PostalCode).NotEmpty().WithMessage("Please specify a postal code").Matches(new Regex(@"^A-\d{4}"));
			RuleFor(recipient => recipient.City).NotEmpty().WithMessage("Please specify a city").Matches(new Regex(@"[A-Z][A-Za-z -]"));
		}
	}
}
