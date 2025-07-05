using System.ComponentModel.DataAnnotations;

namespace WebShopWebApi.DTOs
{
    public class ImageResponseDto
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public bool IsMain { get; set; }
    }
}
