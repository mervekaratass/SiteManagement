using Microsoft.EntityFrameworkCore;
using SiteManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Data.Repository;

public class MessageRepository : GenericRepository<Message>, IMessageRepository
{
    private readonly SiteDbContext dbContext;
    public MessageRepository(SiteDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }
    public Message IsUser(int id)
    {
        return dbContext.Set<Message>().Include(x => x.Receiver).Where(x => x.ReceiverID == id).SingleOrDefault();
    }
    public List<Message> GetAllReceiverMessages()//admine gelnler 
    {
        return dbContext.Set<Message>().Where(x => x.ReceiverID == 1).ToList();
    }
    public List<Message> GetAllSenderMessages()//adminin gönderdikleri
    {
        return dbContext.Set<Message>().Where(x => x.SenderID == 1).ToList();
    }
    public List<Message> GetAllUserReceiverMessages(int receiverid) //usera gelenler
    {
        return dbContext.Set<Message>().Where(x => x.ReceiverID == receiverid).ToList();
    }
   
}
