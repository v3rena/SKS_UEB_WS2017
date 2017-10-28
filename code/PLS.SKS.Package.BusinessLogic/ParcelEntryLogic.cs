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
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IO.Swagger.Models.Recipient, PLS.SKS.Package.BusinessLogic.Entities.Recipient>();
                cfg.CreateMap<IO.Swagger.Models.Parcel, PLS.SKS.Package.BusinessLogic.Entities.Parcel>();
            }
            );

            config.AssertConfigurationIsValid();
            Mapper = config.CreateMapper();
        }

        public void addParcel(Entities.Parcel parcel)
        {
			ParcelValidator validator = new ParcelValidator();
			ValidationResult results = validator.Validate(parcel);

        }

        public AutoMapper.IMapper Mapper { get; set; }

        bool validationSucceeded = results.IsValid;
		IList<ValidationFailure> failures = results.Errors;


    }
  }
