using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Schema;

public class UserResponseSender
{
    public int ReceiverID{ get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
}

