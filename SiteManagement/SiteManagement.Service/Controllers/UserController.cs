using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Base;
using SiteManagement.Data.Entities;
using SiteManagement.Data.Repository;
using SiteManagement.Schema;
using System.Data;
using System.Net.Mail;

namespace SiteManagement.Service;

[Route("api/[controller]")]
[ApiController]

[Authorize(Roles = "Admin")]
public class UserController : ControllerBase
{
    private readonly IValidator<UserRequest> validator;
    //private readonly IValidator<UserUpdateRequest> _validator;
    private readonly IUserRepository repository;
    private readonly IMapper mapper;

    public UserController(IUserRepository repository, IMapper mapper, IValidator<UserRequest> validator/* IValidator<UserUpdateRequest> _validator*/)
    {
        this.validator = validator;
        //this._validator = _validator;
        this.repository = repository;
        this.mapper = mapper;

    }

    [HttpGet]
    public ApiResponse<List<UserResponse>> GetAll()
    {

        var entityList = repository.GetAll();

        var mapped = mapper.Map<List<User>, List<UserResponse>>(entityList);
        return new ApiResponse<List<UserResponse>>(mapped);
    }

    [HttpGet("{id}")]
    public ApiResponse<UserResponse> Get(int id)
    {
        var entity = repository.GetById(id);
        var mapped = mapper.Map<User, UserResponse>(entity);
        return new ApiResponse<UserResponse>(mapped);
    }

    //okunup okunmamasına göre
    [HttpGet("GetByTcNo")]
    public ApiResponse<UserResponse> GetByTcNo(string TcNo)
    {
        var entity = repository.GetbyFilter(x => x.TcNo == TcNo).SingleOrDefault();

        var mapped = mapper.Map<User, UserResponse>(entity);
        return new ApiResponse<UserResponse>(mapped);
    }

    //o user id sine sahip olanları getir
    [HttpGet("GetByUserName")]
    public ApiResponse<List<UserResponse>> GetByUserName(string name)
    {
        var entityList = repository.GetbyFilter(x => x.Name == name);

        var mapped = mapper.Map<List<User>, List<UserResponse>>(entityList);
        return new ApiResponse<List<UserResponse>>(mapped);
    }

    [HttpPost]
    public ApiResponse Post([FromBody] UserRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsValid)
        {
            var tcid = repository.IsTcNo(request.TcNo);
            var apartid = repository.IsApartId(request.ApartmentID);
            if (tcid == null && apartid != null)
            {
                var entity = mapper.Map<UserRequest, User>(request);
                entity.Password = PasswordGenerator.GeneratePassword();
                entity.Role = "User";
                repository.Insert(entity);
                repository.Save();
                return new ApiResponse();
            }
            else if (tcid != null)
            {
                //return new ApiResponse(false, "Böyle bir tc ye sahip kullanıcı mevcut. ");
                return new ApiResponse(false, "There is a user with this tc. ");
            }
            else
            {
                //return new ApiResponse(false, "Bu apartman id sine sahip bir apartman mevcut değil");
                return new ApiResponse(false, "There is no apartment with this apartment id");
            }
        }
        else
        {
            return new ApiResponse(result.Errors);
        }

    }



    [HttpPut("{id}")]
    public ApiResponse Put(int id, [FromBody] UserRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsValid)
        {

            User existing = repository.GetById(id);
            if (existing != null)
            {
                var apartid = repository.IsApartId(request.ApartmentID);
                if (apartid != null)
                {
                    var entity = mapper.Map<UserRequest, User>(request);
                    existing.Name = entity.Name;
                    existing.Email = entity.Email;
                    existing.Surname = entity.Surname;
                    existing.PhoneNumber = entity.PhoneNumber;
                    existing.ApartmentID = entity.ApartmentID;
                    existing.CarInfo = entity.CarInfo;
                    existing.TcNo = entity.TcNo;
                   



                    repository.Update(existing);
                    repository.Save();
                    return new ApiResponse();
                }
                else
                {
                    //    return new ApiResponse(false, "Böyle bir daire mevcut değil");
                    return new ApiResponse(false, "Such an apartment does not exist");
                }
            }
            else
            {
                //return new ApiResponse(false, "Böyle bir kullanıcı mevcut değil");
                return new ApiResponse(false, "No such user exists");
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
            return new ApiResponse("No such user information available");
            //return new ApiResponse("Böyle bir kullanıcı bilgisi mevcut değil");
        }

    }
}
