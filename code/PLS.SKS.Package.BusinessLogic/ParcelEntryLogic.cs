using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using static PLS.SKS.Package.BusinessLogic.Validator;

namespace PLS.SKS.Package.BusinessLogic
{
    public class ParcelEntryLogic : Interfaces.IParcelEntryLogic
    {
        public ParcelEntryLogic()
        {

        }

        public void addParcel(Entities.Parcel parcel)
        {
			ParcelValidator validator = new ParcelValidator();
			ValidationResult results = validator.Validate(parcel);

			bool validationSucceeded = results.IsValid;
			IList<ValidationFailure> failures = results.Errors;

		}
    }
}
