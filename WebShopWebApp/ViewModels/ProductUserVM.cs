namespace WebShopWebApp.ViewModels
{
    public class ProductUserVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public string? Brand { get; set; }

        public List<string> Images { get; set; } = new();
    }
}
