using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Schema;

public class BankRequest
{
    public int UserID { get; set; }
    public string CreditCardNumber { get; set; } = null!;

    public decimal Balance { get; set; }
   
}
