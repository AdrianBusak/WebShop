using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopWebApi.DTOs
{
    public class CartResponseDto
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "decimal(18, 0)")]
        public decimal TotalPrice { get; set; }
        public List<CartItemDto> CartItems { get; set; } = [];
    }
}
