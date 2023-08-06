using FluentValidation;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Schema;
public class MessageRequestValidators : AbstractValidator<MessageRequest>
{
    public MessageRequestValidators()
    {
        RuleFor(x => x.ReceiverMail).NotEmpty().WithMessage("The user mail of the person to whom the message will be sent is required.");
        RuleFor(x => x.Content).NotEmpty().WithMessage("Message content cannot be empty");


    }
}
