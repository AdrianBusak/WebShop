using System.ComponentModel.DataAnnotations;

namespace WebShopWebApp.ViewModels
{
    public class CartItemVM
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        public int ProductId { get; set; }
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
    }
}
