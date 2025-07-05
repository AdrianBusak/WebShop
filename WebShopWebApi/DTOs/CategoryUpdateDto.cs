using System.ComponentModel.DataAnnotations;

namespace WebShopWebApi.DTOs
{
    public class CategoryUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
