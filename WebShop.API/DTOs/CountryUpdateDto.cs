using System.ComponentModel.DataAnnotations;

namespace WebShop.API.DTOs
{
    public class CountryUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
