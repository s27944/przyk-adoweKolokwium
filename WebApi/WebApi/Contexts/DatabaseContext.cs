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

        //Tabela ma dwa klucze obce i jeden główny więc
        //nie trzeba tego konfigurować
        //modelBuilder.Entity<Order>()
        //.HasKey(order => order.ID);

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
        
        //Dane do tabel ========================================
        
        modelBuilder.Entity<Client>().HasData(new List<Client>
        {
            new Client
            {
                clientID = 1,
                FirstName = "Jacek",
                LastName = "Sasin"
            }
        });
        
        modelBuilder.Entity<Status>().HasData(new List<Status>
        {
            new Status
            {
                statusID = 1,
                statusName = "Utworzone"
            },
            
            new Status
            {
            statusID = 2,
            statusName = "Zrealizowane"
            }
        });
        
        modelBuilder.Entity<Product>().HasData(new List<Product>
        {
            new Product
            {
                productID = 1,
                productName = "Czekolada",
                productPrice = 5
            },
        });
        
        modelBuilder.Entity<Order>().HasData(new List<Order>
        {
            new Order
            {
                ID = 1,
                CreatedAt = DateTime.Now,
                FulfilledAt = null,
                ClientID = 1,
                StatusID = 1
            },
        });
        
        modelBuilder.Entity<Product_Order>().HasData(new List<Product_Order>
        {
            new Product_Order
            {
                ProductId = 1,
                OrderId = 1,
                amount = 5
            },
        });
    }
    
}