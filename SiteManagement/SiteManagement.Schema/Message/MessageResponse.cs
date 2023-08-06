using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Schema;

public class MessageResponse
{
   public int Id { get; set; }
    public string ReceiverID { get; set; }
    public string Content { get; set; }
    public bool Status { get; set; }//read unread

}
