using Microsoft.AspNetCore.Mvc.Rendering;
using WebShop.DAL.Models;

namespace WebShop.MVC.ViewModels
{
    public class ProductEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
        public string? Brand { get; set; }

        public List<int> SelectedCountriesIds { get; set; } = new();
        public List<int> SelectedImagesIds { get; set; } = new();
        public List<Image>? Images { get; set; } = new();
        public List<IFormFile> UploadedImages { get; set; } = new();

    }
}
