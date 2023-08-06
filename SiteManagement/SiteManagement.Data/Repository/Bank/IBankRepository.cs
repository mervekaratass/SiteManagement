using SiteManagement.Base;
using SiteManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Data.Repository;

public interface IBankRepository:IGenericRepository<Bank>
{
    List<Bank> GetByUser(int UserId);
    User IsUser(int userid);


}
