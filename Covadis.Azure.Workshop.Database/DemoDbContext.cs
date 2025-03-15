using Covadis.Azure.Database.Models;

using Microsoft.EntityFrameworkCore;

namespace Covadis.Azure.Database;

public class DemoDbContext : DbContext
{
    public DemoDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.ArticleNumber)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");
        });
    }
}
