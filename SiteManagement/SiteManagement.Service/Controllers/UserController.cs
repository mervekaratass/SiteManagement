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
    private readonly IUserRepository repository;
    private readonly IMapper mapper;

    public UserController(IUserRepository repository, IMapper mapper, IValidator<UserRequest> validator)
    {
        this.validator = validator;
        this.repository = repository;
        this.mapper = mapper;

    }

    [HttpGet]
    public ApiResponse<List<UserResponse>> GetAll()
    {
        //bring them all
        var entityList = repository.GetAll();

        var mapped = mapper.Map<List<User>, List<UserResponse>>(entityList);
        return new ApiResponse<List<UserResponse>>(mapped);
    }

    [HttpGet("{userid}")]
    public ApiResponse<UserResponse> Get(int userid)
    { //fetch by user id
        var user = repository.GetById(userid);
        if (user != null)
        {
            var entity = repository.GetById(userid);
            var mapped = mapper.Map<User, UserResponse>(entity);
            return new ApiResponse<UserResponse>(mapped);
        }
        else
        {
            return new ApiResponse<UserResponse>("There is no user with this user id");
        }
    }

   
    [HttpGet("GetByTcNo")]
    public ApiResponse<UserResponse> GetByTcNo(string TcNo)
    {
       // fetch the user with tc number
        var tc=repository.IsTcNo(TcNo);
        if (tc != null)
        {
            var entity = repository.GetbyFilter(x => x.TcNo == TcNo).SingleOrDefault();

            var mapped = mapper.Map<User, UserResponse>(entity);
            return new ApiResponse<UserResponse>(mapped);
        }
        else
        {
            return new ApiResponse<UserResponse>("There is no user with this tc number");
        }
    }

  
    [HttpGet("GetByUserName")]
    public ApiResponse<List<UserResponse>> GetByUserName(string name)
    {
        // fetch the user with username
        var user = repository.GetbyFilter(x => x.Name == name);
        if (user != null)
        {
            var entityList = repository.GetbyFilter(x => x.Name == name);

            var mapped = mapper.Map<List<User>, List<UserResponse>>(entityList);
            return new ApiResponse<List<UserResponse>>(mapped);
        }
        else
        {

            return new ApiResponse<List<UserResponse>>("There is no user with this name");
        }
    }

    [HttpPost]
    public ApiResponse Post([FromBody] UserRequest request)
    {  //add user
        var result = validator.Validate(request);
        if (result.IsValid)
        {
            var tcid = repository.IsTcNo(request.TcNo);

            var email = repository.IsMail(request.Email);
            var apartid = repository.IsApartId(request.ApartmentID);
            if (tcid == null && apartid != null && email==null)
            {
                var entity = mapper.Map<UserRequest, User>(request);
                entity.Password = PasswordGenerator.GeneratePassword();
                entity.Role = "User";
                repository.Insert(entity);
                repository.Save();
                return new ApiResponse(true, "User created successfully");
            }
            else if (tcid != null)
            {
                return new ApiResponse(false, "There is a user with this tc. ");
            }
            else if (email != null)
            {
                return new ApiResponse(false, "There is a user with this email. ");
            }
            else
            {
                return new ApiResponse(false, "There is no apartment with this apartment id");
            }
        }
        else
        {
            return new ApiResponse(result.Errors);
        }

    }



    [HttpPut("{userid}")]
    public ApiResponse Put(int userid, [FromBody] UserRequest request)
    {    //update by user id
        var result = validator.Validate(request);
        if (result.IsValid)
        {

            User existing = repository.GetById(userid);
            if (existing != null)
            {
                List<User> users = repository.GetAll();             
                users.RemoveAll(u => u.TcNo == existing.TcNo);
                //let's copy users
                var usersCopy = new List<User>(users);


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

                    foreach (var item in usersCopy)
                    {
                        if (item.TcNo == existing.TcNo)
                        {
                            return new ApiResponse(false, "There is a user with this tc.");
                            
                        }
                        else if(item.Email==existing.Email)
                        {
                            return new ApiResponse(false, "There is a user with this email.");
                        }
                    }

                    repository.Update(existing);
                    repository.Save();
                    return new ApiResponse(true, "User successfully updated");
                }
                else
                {
                    
                    return new ApiResponse(false, "Such an apartment does not exist");
                }
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


    [HttpDelete("{userid}")]
    public ApiResponse Delete(int userid)
    {
        //delete by user id
        var model = repository.GetById(userid);
        if (model != null)
        {
            repository.DeleteById(userid);
            repository.Save();
            return new ApiResponse(true, "User deleted");
        }
        else
        {
            return new ApiResponse("No such user information available");
            
        }

    }
}
