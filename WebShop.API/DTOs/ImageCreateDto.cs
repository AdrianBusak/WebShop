namespace WebShop.API.DTOs
{
    public class ImageCreateDto
    {
        public string Content { get; set; } = null!;

        public bool? IsMain { get; set; }

        public int? ProductId { get; set; }
    }
}
