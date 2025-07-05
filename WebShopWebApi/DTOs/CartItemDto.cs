using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopWebApi.DTOs
{
    public class CartItemDto
    {
        [Required]
        public int CartId { get; set; }
        [Required]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18, 0)")]
        public decimal UnitPrice { get; set; }
    }
}
