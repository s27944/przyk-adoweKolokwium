using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models;

[Table("Product_Order")]
public class Product_Order
{
    [ForeignKey("Product")]
    [Column("ProductID")]
    public int ProductId { get; set; }
    public Product Product { get; set; }

    [ForeignKey("Order")]
    [Column("OrderID")]
    public int OrderId { get; set; }
    public Order Order { get; set; }
    
    [Column("Amount")]
    public int amount { get; set; }
}