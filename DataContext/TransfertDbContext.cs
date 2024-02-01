using ForSendKH.Models;
using ForSendKH.Models.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForSendKH.Data.DataContext
{
	public class TransfertDbContext : IdentityDbContext<ApiUser>
    {
        public TransfertDbContext(DbContextOptions<TransfertDbContext> options) : base(options)
        {
        }

        public DbSet<Transfert> Transferts { get; set; }
        public DbSet<Expediteur> Expediteurs { get; set; }
        public DbSet<Beneficiere> Beneficieres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }

    }
}

