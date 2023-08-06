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
    {

        var entityList = repository.GetAll();

        var mapped = mapper.Map<List<Message>, List<MessageResponse>>(entityList);
        return new ApiResponse<List<MessageResponse>>(mapped);
    }

    [HttpGet("{id}")]
    public ApiResponse<MessageResponse> Get(int id)
    {
        var entity = repository.GetById(id);
        var mapped = mapper.Map<Message, MessageResponse>(entity);
        return new ApiResponse<MessageResponse>(mapped);
    }

    //okunup okunmamasına göre
    [HttpGet("GetByStatus")]
    public ApiResponse<List<MessageResponse>> GetByStatus(bool Status)
    {
        var entityList = repository.GetbyFilter(x => x.Status == Status);

        var mapped = mapper.Map<List<Message>, List<MessageResponse>>(entityList);
        return new ApiResponse<List<MessageResponse>>(mapped);
    }

    //o user id sine sahip olanları getir
    [HttpGet("GetByUserId")]
    public ApiResponse<List<MessageResponse>> GetByUserId(int id)
    {
        var entityList = repository.GetbyFilter(x => x.ReceiverID == id);

        var mapped = mapper.Map<List<Message>, List<MessageResponse>>(entityList);
        return new ApiResponse<List<MessageResponse>>(mapped);
    }



    //admine gelen mesajlar
    [HttpGet("GetByReceiverMessageAdmin")]
    public ApiResponse<List<UserResponseReceiver>> GetByReceiverMessageAdmin()
    {

        var entityList = repository.GetAllReceiverMessages();

        var mapped = mapper.Map<List<Message>, List<UserResponseReceiver>>(entityList);
        return new ApiResponse<List<UserResponseReceiver>>(mapped);
    }
    //adminin gönderdikleri
    [HttpGet("GetBySenderMessageAdmin")]
    public ApiResponse<List<UserResponseSender>> GetBySenderMessageAdmin()
    {

        var entityList = repository.GetAllSenderMessages();

        var mapped = mapper.Map<List<Message>, List<UserResponseSender>>(entityList);
        return new ApiResponse<List<UserResponseSender>>(mapped);
    }



    [HttpPost]
    public ApiResponse Post([FromBody] MessageRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsValid)
        {
            var id = repository.IsUser(request.ReceiverID);
            if (id != null)
            {
                var entity = mapper.Map<MessageRequest, Message>(request);
                entity.SenderID = 1; //yani admin
                entity.Status = false;
                entity.Date = DateTime.Now.Date;
                repository.Insert(entity);
                repository.Save();
                return new ApiResponse();
            }
            else
            {
                //return new ApiResponse(false, "Böyle bir alıcı mevcut değil");
                return new ApiResponse(false, "Such a receiver does not exist.");
            }
            

        }
        else
        {
            return new ApiResponse(result.Errors);
        }
    }



    [HttpPut("{id}")]
    public ApiResponse Put(int id, [FromBody] MessageRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsValid)
        {

            Message existing = repository.GetById(id);
            if (existing != null)
            {
                var entity = mapper.Map<MessageRequest, Message>(request);
                existing.Status = false;
                existing.SenderID = 1;
                existing.ReceiverID = entity.ReceiverID;
                existing.Content = entity.Content;
                existing.Date = DateTime.Now.Date;

                repository.Update(existing);
                repository.Save();
                return new ApiResponse();
            }
            else
            {
                //return new ApiResponse(false, "Böyle bir mesaj bilgisi mevcut değil");
                return new ApiResponse(false, "No such message information available");
            }

            ////repository.Insert(entity);
            //repository.Update(entity);
            //repository.Save();
            //    return new ApiResponse();
        }
        else
        {
            return new ApiResponse(result.Errors);
        }
    }



    [HttpDelete("{id}")]
    public ApiResponse Delete(int id)
    {
        var model = repository.GetById(id);
        if (model != null)
        {
            repository.DeleteById(id);
            repository.Save();
            return new ApiResponse();
        }
        else
        {
            //return new ApiResponse( "Böyle bir mesaj bilgisi mevcut değil");
            return new ApiResponse( "No such message information available");
        }

    }
}
