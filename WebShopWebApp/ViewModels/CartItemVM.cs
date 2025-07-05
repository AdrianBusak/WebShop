﻿namespace WebShopWebApp.ViewModels
{
    public class CartItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
