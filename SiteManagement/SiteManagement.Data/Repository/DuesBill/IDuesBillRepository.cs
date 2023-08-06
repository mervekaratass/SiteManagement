using SiteManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Data.Repository;

public interface IDuesBillRepository : IGenericRepository<DuesBill>
{
    Apartment isApartment(int id);
    Apartment isApartmentBlock(int blockno, int apartno);


   


}
