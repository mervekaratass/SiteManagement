using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Schema;
 public class BankRequestValidators : AbstractValidator<BankRequest>
 {
    public BankRequestValidators()
    {
        RuleFor(x => x.UserID).NotEmpty().WithMessage("User id is required.");
        RuleFor(x => x.CreditCardNumber).NotEmpty().WithMessage("Credit card number is required").Length(16).WithMessage("Credit card number must be 16 digits");
        RuleFor(x => x.Balance).NotNull().WithMessage("Balance is required");

    }
 }

