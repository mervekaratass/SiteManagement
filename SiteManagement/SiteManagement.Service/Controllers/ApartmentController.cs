using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Base;
using SiteManagement.Data.Entities;
using SiteManagement.Data.Repository;
using SiteManagement.Schema;
using System;

namespace SiteManagement.Service;

[Route("api/[controller]")]
[ApiController]

[Authorize(Roles = "Admin")]
public class ApartmentController : ControllerBase
{
    private readonly IValidator<ApartmentRequest> validator;
    private readonly IApartmentRepository repository;
    private readonly IMapper mapper;

    public ApartmentController(IApartmentRepository repository, IMapper mapper, IValidator<ApartmentRequest> validator)
    {
        this.validator = validator;
        this.repository = repository;
        this.mapper = mapper;

    }

    [HttpGet]
    
    public ApiResponse<List<ApartmentResponse>> GetAll()
    {
        //bring them all
        var entityList = repository.GetAll();

        var mapped = mapper.Map<List<Apartment>, List<ApartmentResponse>>(entityList);
        return new ApiResponse<List<ApartmentResponse>>(mapped);
    }

    [HttpGet("{apartmentid}")]
    public ApiResponse<ApartmentResponse> Get(int apartmentid)
    {//fetch by apartment id
        var model = repository.GetById(apartmentid);
        if (model != null)
        {
            var entity = repository.GetById(apartmentid);
            var mapped = mapper.Map<Apartment, ApartmentResponse>(entity);
            return new ApiResponse<ApartmentResponse>(mapped);
        }
        else
        {

            return new ApiResponse<ApartmentResponse>("No such apartment information is available");
        }
    }


    [HttpPost]
    public ApiResponse Post([FromBody] ApartmentRequest request)
    {     //Adding apartment information
           var result = validator.Validate(request);
           if (result.IsValid)
            {
                  var entity = mapper.Map<ApartmentRequest, Apartment>(request);
                  var model = repository.GetByApartmentAndBlockNumber(request.ApartmentNumber, request.BlockNumber);
                  if (model == null)
                  {
                          repository.Insert(entity);
                         repository.Save();
                        return new ApiResponse("The apartment successfully added");
                   }
                   else
                   {
                //return new ApiResponse(false, "Bu daire zaten mevcut.");
                return new ApiResponse(false, "This apartment already exists");
            }

            }
            else
            {           
              return new ApiResponse(result.Errors);         
             }
        
    }
      
    
       [HttpPut("{apartmentid}")]
        public ApiResponse Put(int apartmentid, [FromBody] ApartmentRequest request)
        {     //update by apartment id
        var result = validator.Validate(request);
          if (result.IsValid)
          {

               Apartment existing = repository.GetById(apartmentid);
               if (existing != null)
               {
                    var entity = mapper.Map<ApartmentRequest, Apartment>(request);

                    existing.ApartmentNumber = entity.ApartmentNumber;
                    existing.BlockNumber = entity.BlockNumber;
                    existing.FloorNumber = entity.FloorNumber;
                    existing.Type = entity.Type;
                    existing.Situation = entity.Situation;
                    existing.OwnerOrTenant = entity.OwnerOrTenant;

                    repository.Update(existing);
                    repository.Save();
                    return new ApiResponse("the apartment successfully updated");

           
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


    #region Deneme
    //[HttpPut("{apartmentNumber}/{blockNumber}")]
    //public ApiResponse Put(int apartmentNumber, int blockNumber, [FromBody] ApartmentRequest request)
    //{
    //    var entity = mapper.Map<ApartmentRequest, Apartment>(request);
    //    repository.UpdateWithApartmenBlockNumber(apartmentNumber, blockNumber, entity);
    //    return new ApiResponse();
    //}


    //[HttpPut("{apartmentNumber}/{blockNumber}")]
    //public ApiResponse Put(int apartmentNumber, int blockNumber, [FromBody] ApartmentRequest request)
    //{


    //    var entity = mapper.Map<ApartmentRequest, Apartment>(request);
    //    var existingApartment = repository.GetByApartmentAndBlockNumber(apartmentNumber, blockNumber);

    //    if (existingApartment != null)
    //    {
    //        entity.Id = existingApartment.Id; // İlgili Apartment Id'sini güncelliyoruz
    //        repository.Update(entity);
    //        repository.Save();
    //        return new ApiResponse();
    //    }
    //    else
    //    {
    //        // Apartman bulunamazsa hata dönebilir veya yeni bir apartman ekleyebilirsiniz.
    //        // Örnek olarak:
    //        return new ApiResponse("Apartman bulunamadı.");
    //    }
    //}


    #endregion

    [HttpDelete("{apartmentid}")]
        public ApiResponse Delete(int apartmentid)
        {
        ////delete by apartment id
            var model = repository.GetById(apartmentid);
           if (model != null)
           {
            repository.DeleteById(apartmentid);
            repository.Save();
            return new ApiResponse("Apartment deleted");
           }
           else
            {
           
            return new ApiResponse("No such apartment information is available");
        }
        }



    }

