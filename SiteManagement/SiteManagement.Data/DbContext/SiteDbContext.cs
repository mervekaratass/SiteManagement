using Microsoft.EntityFrameworkCore;
using SiteManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Data;

public class SiteDbContext : DbContext
{
    public SiteDbContext(DbContextOptions options) : base(options)
    {
    }
     //I created my tables
    public DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<DuesBill> DuesBills { get; set; }
    public DbSet<Apartment> Apartments { get; set; }

    public DbSet<Bank> Banks { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // I defined my configurations here like this
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new MessageConfiguration());
        modelBuilder.ApplyConfiguration(new DuesBillConfiguration());
        modelBuilder.ApplyConfiguration(new ApartmentConfiguration());
        modelBuilder.ApplyConfiguration(new BankConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
