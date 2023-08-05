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

    //burda kendim GetAll mtodunu override ettim
    public List<DuesBill> GetAll()
    {
        return dbContext.Set<DuesBill>().Include(x=>x.Apartment).ToList();
    }

    public Apartment isApartment(int id)
    {
        return dbContext.Set<Apartment>().Where(x => x.Id == id).SingleOrDefault();
    }
    public DuesBill GetById(int id)
    {
        var entity = dbContext.Set<DuesBill>().Include(x => x.Apartment).Where(x => x.Id == id).SingleOrDefault();
        return entity;
    }
    public List<DuesBill> UserGetByApartmentId(int id)
    {
        var entity = dbContext.Set<DuesBill>().Where(x => x.ApartmentID == id).ToList();
        return entity;
    }
    public List<DuesBill> GetbyFilter(Expression<Func<DuesBill, bool>> filter)
    {
        return dbContext.Set<DuesBill>().Include(x=>x.Apartment).Where(filter).ToList();
    }
}
