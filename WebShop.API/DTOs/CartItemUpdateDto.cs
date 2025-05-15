using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebShop.API.DTOs
{
    public class CartItemUpdateDto
    {
        [Required]
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
