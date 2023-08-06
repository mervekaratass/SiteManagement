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
public class DuesBillController : ControllerBase
{
    private readonly IValidator<DuesBillRequest> validator;
    private readonly IDuesBillRepository repository;
    private readonly IMapper mapper;

    public DuesBillController(IDuesBillRepository repository, IMapper mapper, IValidator<DuesBillRequest> validator)
    {
        this.validator = validator;
        this.repository = repository;
        this.mapper = mapper;

    }

    [HttpGet]
    public ApiResponse<List<DuesBillResponse>> GetAll()
    {
        //bring them all
        var entityList = repository.GetAll();

        var mapped = mapper.Map<List<DuesBill>, List<DuesBillResponse>>(entityList);
        return new ApiResponse<List<DuesBillResponse>>(mapped);
    }

    [HttpGet("{id}")]
    public ApiResponse<DuesBillResponse> Get(int id)
    {    //fetch by dues and invoice id
        var entity = repository.GetById(id);
        if (entity != null)
        {
            var mapped = mapper.Map<DuesBill, DuesBillResponse>(entity);
            return new ApiResponse<DuesBillResponse>(mapped);
        }
        else
        {

            return new ApiResponse<DuesBillResponse>("No such payment information is available.");
        }
    }

    [HttpGet("GetByStatus")]
    public ApiResponse<List<DuesBillResponse>> GetByStatus(bool Status)
    {//fetch according to whether paid or not
        var entityList = repository.GetbyFilter(x=>x.Status==Status);
        
        var mapped = mapper.Map<List<DuesBill>, List<DuesBillResponse>>(entityList);
        return new ApiResponse<List<DuesBillResponse>>(mapped);
    }


    [HttpPost]
    public ApiResponse Post([FromBody] DuesBillRequest request)
    {//Adding dues and billing information
        var result = validator.Validate(request);
        if (result.IsValid)
        {
            var id = repository.isApartment(request.ApartmentID);
            if (id != null)
            {


                var entity = mapper.Map<DuesBillRequest, DuesBill>(request);



                repository.Insert(entity);
                repository.Save();
                return new ApiResponse(true,"Added dues and billing information");
            }
            else
            {
                return new ApiResponse(false, "Such an apartment does not exist");
            }
        }
        else
        {
            return new ApiResponse(result.Errors);

        }
    }


    [HttpPut("{id}")]
    public ApiResponse Put(int id, [FromBody] DuesBillRequest request)
    {
        // update according to dues and invoice id
        var result = validator.Validate(request);
        if (result.IsValid)
        {

            var entity = mapper.Map<DuesBillRequest, DuesBill>(request);

            DuesBill existing = repository.GetById(id);
            if (existing != null)
            {
                existing.Status = entity.Status;
                existing.ApartmentID = entity.ApartmentID;
                existing.Water = entity.Water;
                existing.Dues = entity.Dues;
                existing.Electric = entity.Electric;
                existing.NaturalGas = entity.NaturalGas;
                existing.MonthYear = entity.MonthYear;

                var apartment = repository.isApartment(existing.ApartmentID);
                if (apartment != null)
                {

                    repository.Update(entity);
                    repository.Save();
                    return new ApiResponse(true,"The update was performed successfully.");

                }
                else
                {
                    return new ApiResponse(false,"No such apartment information is available");
                }
            }
            else
            {
                return new ApiResponse(false, "No such payment information is available.");
            }

        }
        else
        {
            return new ApiResponse(result.Errors);

        }

    }



    [HttpDelete("{id}")]
    public ApiResponse Delete(int id)
    {    //delete according to dues and invoice id
        var model = repository.GetById(id);
        if (model != null)
        {
            repository.DeleteById(id);
            repository.Save();
            return new ApiResponse(true, "Dues and billing information deleted");
        }
        else
        {
           
            return new ApiResponse("No such payment information is available.");
        }

    }


}
