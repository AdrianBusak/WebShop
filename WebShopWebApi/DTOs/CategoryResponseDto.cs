using System.ComponentModel.DataAnnotations;

namespace WebShopWebApi.DTOs
{
    public class CategoryResponseDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
