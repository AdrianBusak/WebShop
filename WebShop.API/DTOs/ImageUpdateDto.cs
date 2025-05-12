using System.ComponentModel.DataAnnotations;

namespace WebShop.API.DTOs
{
    public class ImageUpdateDto
    {
        [Required]
        public string Content { get; set; } = null!;

        public bool? IsMain { get; set; }

        public int? ProductId { get; set; }
    }
}
