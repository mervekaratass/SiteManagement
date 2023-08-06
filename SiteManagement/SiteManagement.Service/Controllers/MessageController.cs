using AutoMapper;
using Azure.Core;
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
[Authorize(Roles = "Admin")]
public class MessageController : ControllerBase
{
    private readonly IValidator<MessageRequest> validator;
    private readonly IMessageRepository repository;
    private readonly IMapper mapper;

    public MessageController(IMessageRepository repository, IMapper mapper, IValidator<MessageRequest> validator)
    {
        this.validator = validator;
        this.repository = repository;
        this.mapper = mapper;

    }

    [HttpGet]
    public ApiResponse<List<MessageResponse>> GetAll()
    {  //bring them all
        var entityList = repository.GetAll();   

        var mapped = mapper.Map<List<Message>, List<MessageResponse>>(entityList);
        return new ApiResponse<List<MessageResponse>>(mapped);
    }

    [HttpGet("{messageid}")]
    public ApiResponse<MessageResponse> Get(int messageid)
    {   //fetch by message id
        var model = repository.GetById(messageid);
        if (model != null)
        {
            var entity = repository.GetById(messageid);
            var mapped = mapper.Map<Message, MessageResponse>(entity);
            return new ApiResponse<MessageResponse>(mapped);
        }
        else
        {
            return new ApiResponse<MessageResponse>("No such message information available");
        }
    }

    
    [HttpGet("GetByStatus")]
    public ApiResponse<List<MessageResponse>> GetByStatus(bool Status)
    {//fetch by read or not
        var entityList = repository.GetbyFilter(x => x.Status == Status && x.Receiver.Role=="Admin");

        var mapped = mapper.Map<List<Message>, List<MessageResponse>>(entityList);
        return new ApiResponse<List<MessageResponse>>(mapped);
    }

    
    [HttpGet("GetByUserMailReceiverMessages")]
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

    [HttpGet("GetByUserMailSenderMessages")]
    public ApiResponse<List<MessageResponse>> GetByUserMailSenderMessages(string mail)
    {   // messages sent from that mail
        var model = repository.IsUser(mail);
        if (model != null)
        {
            var entityList = repository.GetbyFilter(x => x.Sender.Email == mail);

            var mapped = mapper.Map<List<Message>, List<MessageResponse>>(entityList);
            return new ApiResponse<List<MessageResponse>>(mapped);
        }
        else
        {
            return new ApiResponse<List<MessageResponse>>("Such a mail does not exist.");
        }
    }


    [HttpGet("GetByReceiverMessageAdmin")]
    public ApiResponse<List<UserResponseReceiver>> GetByReceiverMessageAdmin()
    {//fetch incoming messages to admin

        var entityList = repository.GetAllReceiverMessages();


        var mapped = mapper.Map<List<Message>, List<UserResponseReceiver>>(entityList);
        return new ApiResponse<List<UserResponseReceiver>>(mapped);
    }

    [HttpGet("GetBySenderMessageAdmin")]
    public ApiResponse<List<UserResponseSender>> GetBySenderMessageAdmin()
    {
        //fetch messages sent by admin
        var entityList = repository.GetAllSenderMessages();

        var mapped = mapper.Map<List<Message>, List<UserResponseSender>>(entityList);
        return new ApiResponse<List<UserResponseSender>>(mapped);
    }



    [HttpPost]
    public ApiResponse Post([FromBody] MessageRequest request)
    {
        //Message sending process by admin
        var result = validator.Validate(request);
        if (result.IsValid)
        {
            var mail = repository.IsUser(request.ReceiverMail);
            var adminid = repository.IsAdmin().Id;
            if (mail != null)
            {    
                var entity = mapper.Map<MessageRequest, Message>(request);
                entity.SenderID =  adminid;
                entity.ReceiverID = mail.Id;
                entity.Status = false;
                entity.Date = DateTime.Now.Date;
                repository.Insert(entity);
                repository.Save();
                return new ApiResponse(true,"Message sent");
            }
            else
            {
                
                return new ApiResponse(false, "Such a receiver does not exist.");
            }
            

        }
        else
        {
            return new ApiResponse(result.Errors);
        }
    }



    [HttpPut]
    public ApiResponse Put(int messageid, [FromBody] MessageRequest request)
    {
        //update by message id
        var result = validator.Validate(request);
        if (result.IsValid)
        {
           
            Message existing = repository.GetById(messageid);
            if (existing != null)
            {
                var mail = repository.IsUser(request.ReceiverMail);
                if (mail != null)
                {
                    var entity = mapper.Map<MessageRequest, Message>(request);
                    existing.Status = false;
                    existing.SenderID = 1;
                    existing.ReceiverID = mail.Id;
                    existing.Content = entity.Content;
                    existing.Date = DateTime.Now.Date;

                    repository.Update(existing);
                    repository.Save();
                    return new ApiResponse(true,"Message updated");
                }
                else
                {
                    return new ApiResponse(false, "No such receiver mail available");
                }
            }
            else
            {
                
                return new ApiResponse(false, "No such message information available");
            }

        }
        else
        {
            return new ApiResponse(result.Errors);
        }
    }



    [HttpDelete("{messageid}")]
    public ApiResponse Delete(int messageid)
    {
        var model = repository.GetById(messageid);
        if (model != null)
        {
            repository.DeleteById(messageid);
            repository.Save();
            return new ApiResponse(true,"Message deleted");
        }
        else
        {
            return new ApiResponse( true,"No such message information available");
        }

    }
}
