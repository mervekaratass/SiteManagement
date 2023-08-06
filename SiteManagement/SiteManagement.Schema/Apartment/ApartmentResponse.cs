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
    public string Type { get; set; } = null!;
    public bool Situation { get; set; } 
    public string OwnerOrTenant { get; set; } = null!; 
                                                       
    
}
