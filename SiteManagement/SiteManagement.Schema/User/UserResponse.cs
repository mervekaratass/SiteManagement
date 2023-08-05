using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Schema;

public class UserResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string TcNo { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? CarInfo { get; set; }
    public string Password { get; set; } = null!;

    public int ApartmentID { get; set; }

 
    public ICollection<BankResponse2> Banks { get; set; }


    //public ICollection<MessageResponse> SenderMessages { get; set; }
    //public ICollection<MessageResponse> ReceiverMessages { get; set; }
}
