using System.ComponentModel.DataAnnotations;

namespace WebShop.API.DTOs
{
    public class CategoryUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
