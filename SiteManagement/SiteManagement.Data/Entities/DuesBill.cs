using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteManagement.Base.BaseModel;
using Microsoft.Identity.Client;

namespace SiteManagement.Data.Entities
{
    public class DuesBill : IdBaseModel
    {
        
        public int ApartmentID { get; set; } //which apartment does it belong to
        public DateTime MonthYear { get; set; }
        public decimal Dues { get; set; } 
        public decimal Electric { get; set; }
        public decimal Water { get; set; }
        public decimal NaturalGas { get; set; }
        public bool Status { get; set; } //paid-unpaid

        public virtual Apartment Apartment { get; set; }
    }

    public class DuesBillConfiguration : IEntityTypeConfiguration<DuesBill>
    {  //bunları ekledikten sonra dbcontexteki onconfigure me gidip nesne oluşturarak tanıtmama lazım mutlaka !!!!!!!
        public void Configure(EntityTypeBuilder<DuesBill> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.ApartmentID).IsRequired(true);
            builder.HasIndex(x => x.ApartmentID);
            builder.Property(x => x.Dues).HasPrecision(6, 2).HasDefaultValue(0);
            builder.Property(x => x.Electric).HasPrecision(6, 2).HasDefaultValue(0);
            builder.Property(x => x.Water).HasPrecision(6, 2).HasDefaultValue(0);
            builder.Property(x => x.NaturalGas).HasPrecision(6, 2).HasDefaultValue(0);
            builder.Property(x=>x.Status).HasDefaultValue(false);
        }
    }
}
