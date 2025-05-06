using System.ComponentModel.DataAnnotations;

namespace WebShop.API.DTOs
{
    public class ImageResponseDto
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public bool IsMain { get; set; }
    }
}
