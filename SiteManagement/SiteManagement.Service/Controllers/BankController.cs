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
        //bring them all
        var entityList = repository.GetAll();

        var mapped = mapper.Map<List<Bank>, List<BankResponse>>(entityList);
        return new ApiResponse<List<BankResponse>>(mapped);
    }

    [HttpGet("{bankid}")]
    public ApiResponse<BankResponse> Get(int bankid)
    {//fetch by bank id
        var entity = repository.GetById(bankid);
        if (entity != null)
        {
            var mapped = mapper.Map<Bank, BankResponse>(entity);
            return new ApiResponse<BankResponse>(mapped);
        }
        else
        {
            return new ApiResponse<BankResponse>("No such bank information available");
        }
    }


    [HttpPost]
    public ApiResponse Post([FromBody] BankRequest request)
    {//Adding bank information
        var result = validator.Validate(request);
        if (result.IsValid)
        {

            var entity = mapper.Map<BankRequest, Bank>(request);
            var model = repository.GetByUser(request.UserID).SingleOrDefault();
            if (model != null)
            {
                repository.Insert(entity);
                repository.Save();
                return new ApiResponse("The bank info successfully added");
            }
            else
            {
                
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
    { //fetch by user id
        var entity = repository.GetByUser(id).SingleOrDefault();
        if (entity != null)
        {
            var mapped = mapper.Map<Bank, BankResponse>(entity);
            return new ApiResponse<BankResponse>(mapped);
        }
        else {
           ;
            return new ApiResponse<BankResponse>("No such user exists");
        }
    }


    [HttpPut("{Bankid}")]
    public ApiResponse Put(int Bankid, [FromBody] BankRequest request)
    {   //update by bank id
        var result = validator.Validate(request);
        if (result.IsValid)
        {

            Bank existing = repository.GetById(Bankid);
            if (existing != null)
            {
                var entity = mapper.Map<BankRequest, Bank>(request);
                existing.Balance = 0;
                existing.CreditCardNumber = entity.CreditCardNumber;
                existing.UserID = entity.UserID;

                var user = repository.IsUser(existing.UserID);
                if (user!=null){
                    repository.Update(existing);
                    repository.Save();
                    return new ApiResponse(false,"Bank information updated");
                }
                else
                {
                    return new ApiResponse(false,"There is no user with this user id");
                }
            }
            else
            {
                
                return new ApiResponse(false, "No such bank information available");
            }
        }
        else
        {
            return new ApiResponse(result.Errors);
        }
      
    }




    [HttpDelete("{id}")]
    public ApiResponse Delete(int id)
    {
        //deleet by bank id
        var model = repository.GetById(id);
        if (model!=null)
        {
            repository.DeleteById(id);
            repository.Save();
            return new ApiResponse(true,"Bank info deleted");
        }
        else
        {
           
            return new ApiResponse(false,"No such bank information available");
        }
       
    }
}
