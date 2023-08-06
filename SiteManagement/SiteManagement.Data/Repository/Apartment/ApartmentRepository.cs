using Microsoft.EntityFrameworkCore;
using SiteManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Data.Repository;

public class ApartmentRepository : GenericRepository<Apartment>, IApartmentRepository
{
    private readonly SiteDbContext dbContext;
    public ApartmentRepository(SiteDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }

    public Apartment GetByApartmentAndBlockNumber(int apartmentNumber, int blockNumber)
    {
        return dbContext.Apartments.FirstOrDefault(a => a.ApartmentNumber == apartmentNumber && a.BlockNumber == blockNumber);
    }

    //burda kendim GetAll mtodunu override ettim
    //public List<Apartment> GetAll()
    //{
    //    return dbContext.Set<Apartment>().Include(x => x.DuesBills).ToList();
    //}

    //public Apartment GetById(int id)
    //{
    //    return dbContext.Set<Apartment>().Include(x => x.Accounts).ThenInclude(x => x.Transactions).FirstOrDefault(x => x.CustomerNumber == id);
    //}

   

}
