using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using static PLS.SKS.Package.BusinessLogic.Validator;
using PLS.SKS.Package;

namespace PLS.SKS.Package.BusinessLogic
{
    public class HopArrivalLogic : Interfaces.IHopArrivalLogic
    {

        public void scanParcel(Entities.Parcel parcel, string code)
        {
			ParcelValidator validator = new ParcelValidator();
			ValidationResult results = validator.Validate(parcel);

			bool validationSucceeded = results.IsValid;
			IList<ValidationFailure> failures = results.Errors;

		}

    }
}
