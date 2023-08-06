using Microsoft.EntityFrameworkCore;
using SiteManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Data.Repository;

public interface IApartmentRepository:IGenericRepository<Apartment>
{
     Apartment GetByApartmentAndBlockNumber(int apartmentNumber, int blockNumber);



}
