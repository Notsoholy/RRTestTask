using Microsoft.EntityFrameworkCore;
using RRTestTask.Domain;
using RRTestTask.Infrastructure.Extensions;

namespace RRTestTask.Infrastructure
{
    public sealed class RRTestTaskDbContext : DbContext
    {
        public DbSet<PriceItem> PriceItems { get; set; }

        public RRTestTaskDbContext(DbContextOptions<RRTestTaskDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigurePriceItem();
        }
    }
}
