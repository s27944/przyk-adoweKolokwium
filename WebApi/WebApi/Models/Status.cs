using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models;


[Table("Status")]
public class Status
{
    [Key]
    [Column("ID")]
    public int statusID { get; set; }
    
    [Column("Name")]
    [MaxLength(50)]
    public string statusName { get; set; }
    
    public IEnumerable<Order> Order;
}