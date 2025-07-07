using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebShop.DAL.Models;

namespace WebShopWebApp.ViewModels
{
    public class ProductEditVM
    {
        public int Id { get; set; }
        [Display(Name = "Product Name")]
        public string Name { get; set; } = null!;
        [Display(Name = "Product Description")]
        public string? Description { get; set; }
        [Display(Name = "Product Price")]
        public decimal Price { get; set; }
        [Display(Name = "Product Category")]
        public int? CategoryId { get; set; }
        [Display(Name = "Product Brand")]
        public string? Brand { get; set; }

        [Display(Name = "Product Countries")]
        public List<int> SelectedCountriesIds { get; set; } = new();
        public List<int> SelectedImagesIds { get; set; } = new();
        [Display(Name = "Product Images")]
        public List<Image>? Images { get; set; } = new();
        public List<IFormFile> UploadedImages { get; set; } = new();

    }
}
