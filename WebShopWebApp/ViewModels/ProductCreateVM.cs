using System.ComponentModel.DataAnnotations;

namespace WebShopWebApp.ViewModels
{
    public class ProductCreateVM
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Brand { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }


        public List<IFormFile> UploadedImages { get; set; } = new();
        public List<int> SelectedCountriesIds { get; set; } = new();
    }
}
