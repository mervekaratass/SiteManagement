using AutoMapper;
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

[Authorize(Roles ="User,Admin")]


public class UserDuesBillController : ControllerBase
{
    private readonly IDuesBillRepository repository;
    private readonly IMapper mapper;

    public UserDuesBillController(IDuesBillRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;

    }



    [HttpGet]
    public ApiResponse<List<DuesBillResponse>> Get(int apartmentno,int blockno)
    {//Retrieve dues and invoice information by apartment number and block number
        var apartment = repository.isApartmentBlock(blockno,apartmentno);

        if (apartment != null)
        {
            var entity = repository.GetbyFilter(x=>x.ApartmentID==apartment.Id);
            var mapped = mapper.Map<List<DuesBill>, List<DuesBillResponse>>(entity);
            return new ApiResponse<List<DuesBillResponse>>(mapped);
        }
        else
        {
            return new ApiResponse<List<DuesBillResponse>>("Such an apartment does not exist");
        }
    }
}
