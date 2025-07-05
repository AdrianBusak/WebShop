using System.ComponentModel.DataAnnotations;

namespace WebShopWebApi.DTOs
{
    public class CountryResponseDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
