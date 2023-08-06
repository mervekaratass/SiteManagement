using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Schema;

public class MessageResponse
{
    //public int SenderID { get; set; }
    public int ReceiverID { get; set; }
    public string Content { get; set; }
    public bool Status { get; set; }
    //public User Sender { get; set; }
    //public User Receiver { get; set; }
}
