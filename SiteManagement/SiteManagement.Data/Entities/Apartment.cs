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

        //public int ID { get; set; }
        public int ApartmentNumber { get; set; }
        public int BlockNumber { get; set; }
        public int FloorNumber { get; set; }
        public string Type { get; set; } = null!;//2+1 3+1
        public bool Situation { get; set; } //dolu boş durumu
        public string? OwnerOrTenant { get; set; }  //daire sahibi veya kiracı
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<DuesBill> DuesBills { get; set; }

    }

    public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {  //bunları ekledikten sonra dbcontexteki onconfigure me gidip nesne oluşturarak tanıtmama lazım mutlaka !!!!!!!
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
