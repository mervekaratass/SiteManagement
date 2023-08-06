using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Schema;

public class UserResponseReceiver
{

    public int SenderID { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
}
