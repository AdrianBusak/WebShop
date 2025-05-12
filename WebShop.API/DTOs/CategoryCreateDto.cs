using System.ComponentModel.DataAnnotations;

namespace WebShop.API.DTOs
{
    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
