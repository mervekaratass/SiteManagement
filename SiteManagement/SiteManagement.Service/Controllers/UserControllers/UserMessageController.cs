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
    public ApiResponse Post(int Senderid,[FromBody] UserMessageRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsValid)
        {

            var entity = mapper.Map<UserMessageRequest, Message>(request);
            entity.ReceiverID = 1; //yani admin
            entity.SenderID = Senderid;
            entity.Status = false;
            entity.Date = DateTime.Now.Date;
            repository.Insert(entity);
            repository.Save();
            return new ApiResponse();
        }
        else
        {
            return new ApiResponse(result.Errors);
        }

    }

    //admine gelen mesajlar
    [HttpGet("GetByReceiverMessageUser")]
    public ApiResponse<List<UserResponseReceiver>> GetByReceiverMessageUser(int receiverid)
    {

        var entityList = repository.GetAllUserReceiverMessages(receiverid);

        var mapped = mapper.Map<List<Message>, List<UserResponseReceiver>>(entityList);
        return new ApiResponse<List<UserResponseReceiver>>(mapped);
    }
}
