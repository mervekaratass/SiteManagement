using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Base;
using SiteManagement.Data.Entities;
using SiteManagement.Data.Repository;
using SiteManagement.Schema;

namespace SiteManagement.Service;

[Route("api/[controller]")]
[ApiController]

[Authorize(Roles = "User,Admin")]
public class UserMessageController : ControllerBase
{
    private readonly IValidator<UserMessageRequest> validator;
    private readonly IMessageRepository repository;
    private readonly IMapper mapper;

    public UserMessageController(IMessageRepository repository, IMapper mapper, IValidator<UserMessageRequest> validator)
    {
        this.validator = validator;
        this.repository = repository;
        this.mapper = mapper;

    }
    [HttpPost]
    public ApiResponse Post(string sendermail,[FromBody] UserMessageRequest request)
    {
        var result = validator.Validate(request);
        var adminid = repository.IsAdmin().Id;
        var sender = repository.IsUser(sendermail);
        if (result.IsValid)
        {
            if (sender != null)
            {

                var entity = mapper.Map<UserMessageRequest, Message>(request);
                entity.ReceiverID = adminid;
                entity.SenderID = sender.Id;
                entity.Status = false;
                entity.Date = DateTime.Now.Date;



                repository.Insert(entity);
                repository.Save();
                return new ApiResponse(true, "Message sent");
            }
            else
            {
                return new ApiResponse(false, "This e-mail is not registered in the system.");
            }
        }
        else
        {
            return new ApiResponse(result.Errors);
        }

    }



    [HttpGet("GetByMailReceiverMessages")]
    public ApiResponse<List<MessageResponse>> GetByUserMailReceiverMessages(string mail)
    {   //messages sent to that e-mail
        var model = repository.IsUser(mail);
        if (model != null)
        {
            var entityList = repository.GetbyFilter(x => x.Receiver.Email == mail);

            var mapped = mapper.Map<List<Message>, List<MessageResponse>>(entityList);
            return new ApiResponse<List<MessageResponse>>(mapped);
        }
        else
        {
            return new ApiResponse<List<MessageResponse>>("Such a mail does not exist.");
        }
    }
}
