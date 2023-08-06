using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Schema;

public class UserRequestValidators : AbstractValidator<UserRequest>
{
    public UserRequestValidators()
    {

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required.");
        RuleFor(x => x.TcNo).NotEmpty().WithMessage("TcNo is required.");
        RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("Email is required.");
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required.");
        RuleFor(x => x.TcNo).Length(11).WithMessage("Tc number must be 11 digits");




    }
}
