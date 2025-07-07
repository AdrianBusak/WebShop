using System.ComponentModel.DataAnnotations;

namespace WebShopWebApp.ViewModels
{
    public class ProductDetailsVM
    {
        public int Id { get; set; }
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [Display(Name = "Product Description")]
        public string Description { get; set; }
        [Display(Name = "Product Price")]
        public decimal Price { get; set; }
        [Display(Name = "Product Quantity")]
        public string CategoryName { get; set; }
        [Display(Name = "Product Brand")]
        public string Brand { get; set; }


        public List<string> CountryNames { get; set; } = new();
        public List<string>? Images { get; set; } = new();
    }
}