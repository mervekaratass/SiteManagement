using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Schema;

public class UserRequest
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string TcNo { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? CarInfo { get; set; }
    
    public int ApartmentID { get; set; }
}
