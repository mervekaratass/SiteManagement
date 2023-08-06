using SiteManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Data.Repository;

public interface IMessageRepository : IGenericRepository<Message>
{
    Message IsUser(int id);
    List<Message> GetAllSenderMessages();
    List<Message> GetAllReceiverMessages();
    List<Message> GetAllUserReceiverMessages(int receiverid);
}
