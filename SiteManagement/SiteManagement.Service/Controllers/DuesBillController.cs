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

        var entityList = repository.GetAll();

        var mapped = mapper.Map<List<DuesBill>, List<DuesBillResponse>>(entityList);
        return new ApiResponse<List<DuesBillResponse>>(mapped);
    }

    [HttpGet("{id}")]
    public ApiResponse<DuesBillResponse> Get(int id)
    {
        var entity = repository.GetById(id);
        var mapped = mapper.Map<DuesBill, DuesBillResponse>(entity);
        return new ApiResponse<DuesBillResponse>(mapped);
    }

    [HttpGet("GetByStatus")]
    public ApiResponse<List<DuesBillResponse>> GetByStatus(bool Status)
    {
        var entityList = repository.GetbyFilter(x=>x.Status==Status);
        
        var mapped = mapper.Map<List<DuesBill>, List<DuesBillResponse>>(entityList);
        return new ApiResponse<List<DuesBillResponse>>(mapped);
    }


    [HttpPost]
    public ApiResponse Post([FromBody] DuesBillRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsValid)
        {
            var id = repository.isApartment(request.ApartmentID);
            if (id != null)
            {


                var entity = mapper.Map<DuesBillRequest, DuesBill>(request);



                repository.Insert(entity);
                repository.Save();
                return new ApiResponse();
            }
            else
            {
                //return new ApiResponse(false, "Böyle bir daire mevcut değil");
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

                repository.Update(entity);
                repository.Save();
                return new ApiResponse();
            }
            else
            {
                //return new ApiResponse(false, "Böyle bir ödeme bilgisi mevcut değil");
                return new ApiResponse(false, "No such payment information is available.");
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
            //return new ApiResponse("Böyle bir ödeme bilgisi mevcut değil");
            return new ApiResponse("No such payment information is available.");
        }

    }


}
