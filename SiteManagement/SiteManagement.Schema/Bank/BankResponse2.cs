﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Schema;

public  class BankResponse2
{
    public int Id { get; set; }
    public int UserID { get; set; }
    public string CreditCardNumber { get; set; } = null!;

}
