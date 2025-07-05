using System.ComponentModel.DataAnnotations;

namespace WebShopWebApi.DTOs
{
    public class CountryUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
