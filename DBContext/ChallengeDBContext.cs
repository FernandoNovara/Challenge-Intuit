using Challenge.Backend.Model;
using Microsoft.EntityFrameworkCore;
namespace Challenge.Backend.DBContext
{
    public class ChallengeDBContext : DbContext
    {
        public ChallengeDBContext(DbContextOptions<ChallengeDBContext> options) : base(options)
        {}

        public DbSet<Clients> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Clients>().ToTable("Clients").HasKey(c => c.ClientId);
            modelBuilder.Entity<Clients>(
                    entity =>
                    {
                        entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                        entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                        entity.Property(e => e.CUIT).IsRequired().HasMaxLength(20);
                        entity.Property(e => e.Address).HasMaxLength(150);
                        entity.Property(e => e.Cellphone).HasMaxLength(20);
                        entity.Property(e => e.Email).HasMaxLength(100);
                    }
                );
        }
    }
}
