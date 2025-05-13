using System.ComponentModel.DataAnnotations;

namespace WebShop.API.DTOs
{
    public class CountryResponseDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
