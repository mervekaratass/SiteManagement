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
    public class Apartment : IdBaseModel
    {
        public Apartment()
        {
            Users = new HashSet<User>();
            DuesBills = new HashSet<DuesBill>();
        }

        public int ApartmentNumber { get; set; }
        public int BlockNumber { get; set; }
        public int FloorNumber { get; set; }
        public string Type { get; set; } = null!;//2+1 3+1
        public bool Situation { get; set; } //Condition of the apartment=full or empty
        public string? OwnerOrTenant { get; set; }  //apartmen owner or tenant
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<DuesBill> DuesBills { get; set; }

    }

    public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
       // I am configuring with Fluent Api
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {

           
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id);    
            builder.Property(x => x.ApartmentNumber).IsRequired(true).ValueGeneratedNever();
            builder.Property(x => x.BlockNumber).IsRequired(true).ValueGeneratedNever();
            builder.Property(x => x.FloorNumber).IsRequired(true);
            builder.Property(x => x.Type).IsRequired(true).HasMaxLength(10);
            builder.Property(x => x.Situation).IsRequired(true);
            builder.Property(x => x.OwnerOrTenant);





            builder.HasMany(x => x.Users)
                .WithOne(x => x.Apartment).HasForeignKey(x => x.ApartmentID);

            builder.HasMany(x => x.DuesBills)
                .WithOne(x => x.Apartment).HasForeignKey(x => x.ApartmentID);

        }
    }
}
