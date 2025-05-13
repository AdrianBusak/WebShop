namespace WebShop.API.DTOs
{
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Brand { get; set; }

        public CategoryResponseDto? Category { get; set; }
        public List<ImageResponseDto> Images { get; set; } = new();
    }
}
