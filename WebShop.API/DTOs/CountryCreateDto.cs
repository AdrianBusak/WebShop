using System.ComponentModel.DataAnnotations;

namespace WebShop.API.DTOs
{
    public class CountryCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
