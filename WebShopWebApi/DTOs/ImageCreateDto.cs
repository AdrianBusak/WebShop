using System.ComponentModel.DataAnnotations;

namespace WebShopWebApi.DTOs
{
    public class ImageCreateDto
    {
        [Required]
        public string Content { get; set; } = null!;

        public bool? IsMain { get; set; }

        public int? ProductId { get; set; }
    }
}
