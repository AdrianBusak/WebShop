using System.ComponentModel.DataAnnotations;

namespace WebShopWebApi.DTOs
{
    public class CountryCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
