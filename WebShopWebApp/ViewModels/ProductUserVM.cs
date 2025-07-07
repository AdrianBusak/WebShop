using System.ComponentModel.DataAnnotations;

namespace WebShopWebApp.ViewModels
{
    public class ProductUserVM
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
        [Display(Name = "Brend")]
        public string? Brand { get; set; }

        public List<string> Images { get; set; } = new();
    }
}
