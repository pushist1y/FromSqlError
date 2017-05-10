using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FromSqlError
{
    public class CommonContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConfigurationAccessor.Config.GetConnectionString("Main"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ModelFilter>();
        }

        public DbSet<ModelMain> MainModels { get; set; }

    }
}
