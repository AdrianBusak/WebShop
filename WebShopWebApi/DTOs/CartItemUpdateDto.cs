using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebShopWebApi.DTOs
{
    public class CartItemUpdateDto
    {
        [Required]
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
