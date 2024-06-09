using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models;

[Table("Product")]
public class Product
{
    [Key]
    [Column("ID")]
    public int productID { get; set; }
    
    [Column("Name")]
    [MaxLength(50)]
    public string productName { get; set; }
    
    [Column("Price")]
    public decimal productPrice { get; set; }

    public IEnumerable<Product_Order> Product_Order;
}