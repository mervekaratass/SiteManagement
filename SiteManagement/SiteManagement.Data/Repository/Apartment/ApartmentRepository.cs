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
       // Find apartment with given apartment and block number from apartment table
        return dbContext.Apartments.FirstOrDefault(a => a.ApartmentNumber == apartmentNumber && a.BlockNumber == blockNumber);
    }

   

}
