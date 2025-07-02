using WebShop.DAL.Models;

namespace WebShop.MVC.ViewModels;
public class ProductResponseVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string CategoryName { get; set; }
    public string? Brand { get; set; }

    public List<string> CountryNames { get; set; } = new();
}
