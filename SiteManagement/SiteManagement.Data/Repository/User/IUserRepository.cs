using SiteManagement.Base;
using SiteManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Data.Repository;

public interface IUserRepository : IGenericRepository<User>
{
    User IsTcNo(string tc);
    Apartment IsApartId(int id);
    User IsMail(string mail);





}
