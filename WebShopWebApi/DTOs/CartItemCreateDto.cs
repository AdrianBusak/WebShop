using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopWebApi.DTOs
{
    public class CartItemCreateDto
    {
        [Required]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

    }
}
