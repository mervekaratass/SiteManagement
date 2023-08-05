using SiteManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Schema;

public class ApartmentResponse
{
    public int Id { get; set; }
    public int ApartmentNumber { get; set; }
    public int BlockNumber { get; set; }
    public int FloorNumber { get; set; }
    public string Type { get; set; } = null!;//2+1 3+1
    public bool Situation { get; set; } //dolu boş durumu
    public string OwnerOrTenant { get; set; } = null!; //daire sahibi veya kiracı
                                                       
    //public ICollection<DuesBillResponse> DuesBills { get; set; }
    //public virtual ICollection<User> Users { get; set; }
}
