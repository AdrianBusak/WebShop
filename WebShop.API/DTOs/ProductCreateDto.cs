using System.ComponentModel.DataAnnotations;

namespace WebShop.API.DTOs
{
    public class ProductCreateDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }

        public int? CategoryId { get; set; }
    }
}
