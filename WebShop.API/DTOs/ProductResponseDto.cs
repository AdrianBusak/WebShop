using System.ComponentModel.DataAnnotations;

namespace WebShop.API.DTOs
{
    public class ProductResponseDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Brand { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
    }
}
