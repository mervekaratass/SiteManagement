using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Schema;
public class UserMessageRequestValidators : AbstractValidator<UserMessageRequest>
{
    public UserMessageRequestValidators()
    {
    
        RuleFor(x => x.Content).NotEmpty().WithMessage("Message content cannot be empty");


    }
}

