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
    public class User : IdBaseModel
    {
        public User()
        {
            Banks = new HashSet<Bank>();
            SenderMessages = new HashSet<Message>();
            ReceiverMessages = new HashSet<Message>();

        }

       
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string TcNo { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? CarInfo { get; set; }
        public string Password { get; set; } = null!;

        public int ApartmentID { get; set; }

        public string Role { get; set; } = null!;
        public virtual Apartment Apartment { get; set; }
        public virtual ICollection<Bank> Banks { get; set; }


        public virtual ICollection<Message> SenderMessages { get; set; }
        public virtual ICollection<Message> ReceiverMessages { get; set; }
    }


    public class UserConfiguration : IEntityTypeConfiguration<User>
    {  //I am configuring with Fluent Api
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Surname).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.TcNo).IsRequired(true).HasMaxLength(11);
            builder.HasIndex(x => x.TcNo);
            builder.Property(x => x.Email).IsRequired(true);
            builder.Property(x => x.PhoneNumber).IsRequired(true).HasMaxLength(11);
            builder.Property(x => x.Password).IsRequired(true).HasMaxLength(8);
            builder.Property(x => x.ApartmentID).IsRequired(true);
            builder.HasIndex(x => x.TcNo);
            builder.Property(x => x.Role).IsRequired(true);

            builder.HasMany(x => x.Banks)
                .WithOne(x => x.User).HasForeignKey(x => x.UserID).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.SenderMessages)
                .WithOne(x => x.Sender).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.ReceiverMessages)
                      .WithOne(x => x.Receiver).OnDelete(DeleteBehavior.Cascade);


          


        }
    }
}
