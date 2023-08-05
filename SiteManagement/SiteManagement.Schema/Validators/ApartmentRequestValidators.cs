using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Schema;

public  class ApartmentRequestValidators: AbstractValidator<ApartmentRequest>
{
    public ApartmentRequestValidators()
    {
        RuleFor(x => x.ApartmentNumber).NotEmpty().WithMessage("Apartment number is required.");
        RuleFor(x => x.BlockNumber).NotEmpty().WithMessage("Block number is required");

        RuleFor(x => x.FloorNumber).NotEmpty().WithMessage("Floor number is required.");

        RuleFor(x => x.Type).NotEmpty().WithMessage("Apartment type number is required.");

        RuleFor(x => x.Situation).NotNull().WithMessage("Apartment situation is required");


             RuleFor(x => x.OwnerOrTenant)
               .NotEmpty()
            .When(apartment => apartment.Situation == true)
            .WithMessage("\"Owner or tenant information is required as the flat is occupied.");

    }


}
