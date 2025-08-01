﻿namespace WebShopWebApp.ViewModels
{
    public class AdminSearchVM
    {
        public string Q { get; set; }
        public string OrderBy { get; set; } = "";
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        public int LastPage { get; set; }
        public int FromPager { get; set; }
        public int ToPager { get; set; }
        public string Submit { get; set; }
        public IEnumerable<ProductResponseVM> Products { get; set; }
    }
}
