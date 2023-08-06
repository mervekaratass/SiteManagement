using Microsoft.EntityFrameworkCore;
using SiteManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Data.Repository;

public class DuesBillRepository : GenericRepository<DuesBill>, IDuesBillRepository
{
    private readonly SiteDbContext dbContext;
    public DuesBillRepository(SiteDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }

   
    public List<DuesBill> GetAll()
    {
       // I include its apartments by overriding the GetAll method
        return dbContext.Set<DuesBill>().Include(x=>x.Apartment).ToList();
    }

    public Apartment isApartment(int id)
    {
        //Find apartment with apartment id
        return dbContext.Set<Apartment>().Where(x => x.Id == id).SingleOrDefault();
    }

    public Apartment isApartmentBlock(int blockno,int apartno)
    {
        // Find apartment with blockno and apartmentno
        return dbContext.Set<Apartment>().Where(x => x.BlockNumber == blockno&&x.ApartmentNumber==apartno).SingleOrDefault();
    }
    public DuesBill GetById(int id)
    {
        //Find apartment with duesbill id and include the apartment
        var entity = dbContext.Set<DuesBill>().Include(x => x.Apartment).Where(x => x.Id == id).SingleOrDefault();
        return entity;
    }
  
    public List<DuesBill> GetbyFilter(Expression<Func<DuesBill, bool>> filter)
    {
        //find those who meet the requirement and include apartment
        return dbContext.Set<DuesBill>().Include(x=>x.Apartment).Where(filter).ToList();
    }



}
