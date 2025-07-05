using System.ComponentModel.DataAnnotations;

namespace WebShopWebApi.DTOs
{
    public class ProductCreateDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Brand { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
    }
}
