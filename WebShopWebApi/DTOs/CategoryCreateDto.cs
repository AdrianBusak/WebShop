using System.ComponentModel.DataAnnotations;

namespace WebShopWebApi.DTOs
{
    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
