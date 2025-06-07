using Microsoft.EntityFrameworkCore;

namespace CriptoBiyuya.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        //Para poder comprar criptos desde 0.00001 sin redondear a 0
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.money)
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.crypto_amount)
                    .HasColumnType("decimal(18,8)");
            });
        }

    }
}
