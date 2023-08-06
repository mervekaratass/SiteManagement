using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Schema;

public class DuesBillResponse
{ 
    public int Id { get; set; }
    public DateTime MonthYear { get; set; }
    public decimal Dues { get; set; }
    public decimal Electric { get; set; }
    public decimal Water { get; set; }
    public decimal NaturalGas { get; set; }
    public bool Status { get; set; } //paid or unpaid
    public virtual ApartmentResponse Apartment { get; set; }

}
