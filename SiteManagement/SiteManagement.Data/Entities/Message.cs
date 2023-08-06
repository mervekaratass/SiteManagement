using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteManagement.Base.BaseModel;

namespace SiteManagement.Data.Entities
{
    public class Message : IdBaseModel
    {
       
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
        public virtual User Sender { get; set; }
        public virtual  User Receiver { get; set; }

    }

    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {  //I am configuring with Fluent Api
        public void Configure(EntityTypeBuilder<Message> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.SenderID).IsRequired(true);
            builder.Property(x => x.ReceiverID).IsRequired(true);
            builder.Property(x => x.Content).HasMaxLength(300);
            builder.Property(x=>x.Date).IsRequired(true);

        }
    }
}
