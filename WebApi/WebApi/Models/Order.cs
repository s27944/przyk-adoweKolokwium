using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models;

[Table("Order")]
public class Order
{
    [Key]
    [Column("ID")]
    public int ID { get; set; }

    [Column("CreatedAt")]
    public DateTime CreatedAt { get; set; }

    [Column("FulfilledAt")]
    public DateTime? FulfilledAt { get; set; }

    [ForeignKey("Client")]
    [Column("ClientID")]
    public int ClientID { get; set; }
    public Client Client { get; set; }

    [ForeignKey("Status")]
    [Column("StatusID")]
    public int StatusID { get; set; }
    public Status Status { get; set; }

    public IEnumerable<Product_Order> Product_Order { get; set; }
}
