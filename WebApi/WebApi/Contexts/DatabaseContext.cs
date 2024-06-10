using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Contexts;

public class DatabaseContext : DbContext
{
    public DbSet<Client> Client { get; set; }
    public DbSet<Status> Status { get; set; }
    public DbSet<Product> Product { get; set; }

    public DbSet<Order> Order { get; set; }
    public DbSet<Product_Order> Product_Order { get; set; }

    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Product_Order>()
            .HasKey(po => new { po.ProductId, po.OrderId });

        modelBuilder.Entity<Order>()
            .HasKey(order => new { order.ClientID, order.StatusID });

        modelBuilder.Entity<Product_Order>()
            .HasOne(pc => pc.Product)
            .WithMany(p => p.Product_Order)
            .HasForeignKey(pc => pc.ProductId);

        modelBuilder.Entity<Product_Order>()
            .HasOne(po => po.Order)
            .WithMany(o => o.Product_Order)
            .HasForeignKey(pc => pc.OrderId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Client)
            .WithMany(c => c.Order)
            .HasForeignKey(sc => sc.ClientID);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Status)
            .WithMany(s => s.Order)
            .HasForeignKey(sc => sc.StatusID);
    }
}