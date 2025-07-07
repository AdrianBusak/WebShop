using System.ComponentModel.DataAnnotations;
using WebShop.DAL.Models;

namespace WebShopWebApp.ViewModels;
public class ProductResponseVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    [Display(Name = "Description")]
    public string Description { get; set; }
    [Display(Name = "Price")]
    public decimal Price { get; set; }
    [Display(Name = "Quantity")]
    public string CategoryName { get; set; }
    [Display(Name = "Brand")]
    public string Brand { get; set; }

    public List<string> CountryNames { get; set; } = new();
    public List<string> Images { get; set; } = new();
}
