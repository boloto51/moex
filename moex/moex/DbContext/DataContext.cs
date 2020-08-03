using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using moex.Models;

namespace moex.DbContext
{
    public class DataContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private string connectionString;

        public DataContext() : base()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            connectionString = configuration.GetConnectionString("DefaultConnection").ToString();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Security>().HasKey("TradeDate", "SECID");
            modelBuilder.Entity<Security>().HasMany(s => s.Trades);
            modelBuilder.Entity<Trade>().HasOne(t => t.Security);
        }

        public DbSet<Security> Securities { get; set; }
        public DbSet<Trade> Trades { get; set; }
    }
}
