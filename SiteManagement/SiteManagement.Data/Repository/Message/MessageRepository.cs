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
    public User IsUser(string mail)
    {  //Find user with email
        return dbContext.Set<User>().Where(x => x.Email == mail).SingleOrDefault();
    }
    public List<Message> GetAllReceiverMessages()// messages to admin
    {
        return dbContext.Set<Message>().Include(x=>x.Receiver).Where(x => x.Receiver.Role == "Admin").ToList();
    }
    public List<Message> GetAllSenderMessages()// messages sent by admin
    {
        return dbContext.Set<Message>().Include(x=>x.Sender).Where(x => x.Sender.Role == "Admin").ToList();
    }
    public List<Message> GetAllUserReceiverMessages(int receiverid) //messages to users
    {
        return dbContext.Set<Message>().Where(x => x.ReceiverID == receiverid).ToList();
    }
    public User IsAdmin()
    {
        //Find user with admin role
        return dbContext.Set<User>().Where(x => x.Role == "Admin").SingleOrDefault();
    }


}
