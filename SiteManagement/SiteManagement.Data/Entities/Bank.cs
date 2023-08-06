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
    public class Bank : IdBaseModel
    {
        //public int ID { get; set; }
        public int UserID { get; set; }
        public decimal Balance { get; set; }
        public string CreditCardNumber { get; set; } = null!;

        public virtual User User { get; set; }
    }

    public class BankConfiguration : IEntityTypeConfiguration<Bank>
    {  //bunları ekledikten sonra dbcontexteki onconfigure me gidip nesne oluşturarak tanıtmama lazım mutlaka !!!!!!!
        public void Configure(EntityTypeBuilder<Bank> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserID).IsRequired(true);
            builder.Property(x => x.Balance).IsRequired(true).HasPrecision(15, 4).HasDefaultValue(0);
            builder.Property(x => x.CreditCardNumber).IsRequired(true).HasMaxLength(16);

        }
    }
}
