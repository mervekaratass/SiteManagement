using SiteManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Data.Repository;

public interface IMessageRepository : IGenericRepository<Message>
{
    User IsUser(string mail);
    List<Message> GetAllSenderMessages();
    List<Message> GetAllReceiverMessages();
    List<Message> GetAllUserReceiverMessages(int receiverid);
    User IsAdmin();
}
