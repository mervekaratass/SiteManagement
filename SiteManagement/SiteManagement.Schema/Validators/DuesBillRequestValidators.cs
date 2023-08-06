using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Schema;

   public class DuesBillRequestValidators:AbstractValidator<DuesBillRequest>
{
      public DuesBillRequestValidators()
    {
        RuleFor(x => x.ApartmentID).NotEmpty().WithMessage("Apartment id is required.");
        RuleFor(x => x.MonthYear).NotEmpty().WithMessage("Date is required");



    }

}

