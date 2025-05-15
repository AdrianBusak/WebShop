using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.API.DTOs
{
    public class CartItemCreateDto
    {
        [Required]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

    }
}
