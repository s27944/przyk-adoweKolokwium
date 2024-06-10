using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic.CompilerServices;

namespace WebApi.RequestModels;

public class PostClientOrderModel
{
    [Required]
    public DateTime createdAt { get; set; }
    
    public DateTime fulfilledAt { get; set; }
    
    [Required]
    [MinLength(1)]
    public List<ProductListRequest> products { get; set; }


    public class ProductListRequest
    {
        [Required]
        public int productId { get; set; }
        
        [Required]
        [Range(1, int.MaxValue)]
        public int amount { get; set; }
    }
}