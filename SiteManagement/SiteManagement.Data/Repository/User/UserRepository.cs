﻿using Microsoft.EntityFrameworkCore;
using SiteManagement.Base;
using SiteManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Data.Repository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly SiteDbContext dbContext;
    public UserRepository(SiteDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }
    public User IsTcNo(string tc)
    {
        //Find user with tcno and include the bank
        return dbContext.Set<User>().Where(x => x.TcNo == tc).Include(x => x.Banks).SingleOrDefault();
    }

    public Apartment IsApartId(int id)
    {
        //It was built to see if there is such an apartment.
        return dbContext.Set<Apartment>().Where(x => x.Id == id).SingleOrDefault();
    }

    //burda kendim GetAll mtodunu override ettim
    public List<User> GetAll()
    {
        //Override GetAll method and include the banks
        return dbContext.Set<User>().Include(x => x.Banks).ToList();
    }
    public User GetById(int id)
    {   //Find user with id and include the bank
        var entity = dbContext.Set<User>().Include(x => x.Banks).Where(X => X.Id == id).SingleOrDefault();
        return entity;
    }
     public User IsMail(string mail)
    {
        return dbContext.Set<User>().Where(x => x.Email == mail).SingleOrDefault();
    }
    

    
}
