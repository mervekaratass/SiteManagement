using Microsoft.EntityFrameworkCore;
using SiteManagement.Base;
using SiteManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Data.Repository;

public class BankRepository : GenericRepository<Bank>, IBankRepository
{
    private readonly SiteDbContext dbContext;
    public BankRepository(SiteDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }
    public List<Bank> GetByUser(int UserId)
    {
        return dbContext.Set<Bank>().Include(x => x.User).Where(x=>x.UserID == UserId).ToList();
    }


  
    public List<Bank> GetAll()
    {
        return dbContext.Set<Bank>().Include(x => x.User).ToList();
    }

    public Bank GetById(int id)
    {
        var entity = dbContext.Set<Bank>().Include(x => x.User).Where(x=>x.Id==id).SingleOrDefault();
        return entity;
    }

   
       

         

    

}
