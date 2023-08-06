using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Schema;

public class ApartmentRequest
{
  
    public int ApartmentNumber { get; set; }
    public int BlockNumber { get; set; }
    public int FloorNumber { get; set; }
    public string Type { get; set; } = null!;//2+1 3+1
    public bool Situation { get; set; }  // Condition of the apartment = full or empty
    public string OwnerOrTenant { get; set; } = null!;
}
