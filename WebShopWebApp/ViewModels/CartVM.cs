namespace WebShopWebApp.ViewModels
{
    public class CartVM
    {
        public List<CartItemVM> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
