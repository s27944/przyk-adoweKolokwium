using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models;

[Table("Client")]
public class Client
{
    [Key]
    [Column("ID")]
    public int clientID { get; set; }
    
    [Column("FirstName")]
    [MaxLength(50)]
    public string FirstName { get; set; }
    
    [Column("LastName")]
    [MaxLength(100)]
    public string LastName { get; set; }
    
    public IEnumerable<Order> Order;

}