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
public class BankController : ControllerBase
{
    private readonly IValidator<BankRequest> validator;
    private readonly IBankRepository repository;
    private readonly IMapper mapper;

    public BankController(IBankRepository repository, IMapper mapper, IValidator<BankRequest> validator)
    {
        this.validator = validator;
        this.repository = repository;
        this.mapper = mapper;

    }

    [HttpGet]
    public ApiResponse<List<BankResponse>> GetAll()
    {

        var entityList = repository.GetAll();

        var mapped = mapper.Map<List<Bank>, List<BankResponse>>(entityList);
        return new ApiResponse<List<BankResponse>>(mapped);
    }

    [HttpGet("{id}")]
    public ApiResponse<BankResponse> Get(int id)
    {
        var entity = repository.GetById(id);
        if (entity != null)
        {
            var mapped = mapper.Map<Bank, BankResponse>(entity);
            return new ApiResponse<BankResponse>(mapped);
        }
        else
        {
            return new ApiResponse<BankResponse>("Böyle bir banka bilgisi mevcut değil");
        }
    }


    [HttpPost]
    public ApiResponse Post([FromBody] BankRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsValid)
        {

            var entity = mapper.Map<BankRequest, Bank>(request);
            var model = repository.GetByUser(request.UserID).SingleOrDefault();
            if (model != null)
            {
                repository.Insert(entity);
                repository.Save();
                return new ApiResponse();
            }
            else
            {
                //return new ApiResponse(false, "Böyle bir kullanıcı mevcut değil");
                return new ApiResponse(false, "No such user exists");
            }

        }
        else
        {  
            return new ApiResponse(result.Errors); 
        }


    }

    [HttpGet("GetByUserId")]
    public ApiResponse<BankResponse> GetByUserId(int id)
    {
        var entity = repository.GetByUser(id).SingleOrDefault();
        if (entity != null)
        {
            var mapped = mapper.Map<Bank, BankResponse>(entity);
            return new ApiResponse<BankResponse>(mapped);
        }
        else {
            //return new ApiResponse<BankResponse>("Böyle bir kullanıcı mevcut değil");
            return new ApiResponse<BankResponse>("No such user exists");
        }
    }


    [HttpPut("{id}")]
    public ApiResponse Put(int id, [FromBody] BankRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsValid)
        {

            Bank existing = repository.GetById(id);
            if (existing != null)
            {
                var entity = mapper.Map<BankRequest, Bank>(request);
                existing.Balance = 0;
                existing.CreditCardNumber = entity.CreditCardNumber;
                existing.UserID = entity.UserID;

                repository.Update(existing);
                repository.Save();
                return new ApiResponse();
            }
            else
            {
                //return new ApiResponse(false, "Böyle bir banka bilgisi mevcut değil");
                return new ApiResponse(false, "No such bank information available");
            }
        }
        else
        {
            return new ApiResponse(result.Errors);
        }
        ////repository.Insert(entity);
        //repository.Update(entity);
        //repository.Save();
        //    return new ApiResponse();
    }




    [HttpDelete("{id}")]
    public ApiResponse Delete(int id)
    {
        var model = repository.GetById(id);
        if (model!=null)
        {
            repository.DeleteById(id);
            repository.Save();
            return new ApiResponse();
        }
        else
        {
            //return new ApiResponse("Böyle bir banka bilgisi mevcut değil");
            return new ApiResponse("No such bank information available");
        }
       
    }
}
