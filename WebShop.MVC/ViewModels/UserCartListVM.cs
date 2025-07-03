namespace WebShop.MVC.ViewModels
{
    public class UserCartListVM
    {
        public string Username { get; set; }
        public List<CartItemVM> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
