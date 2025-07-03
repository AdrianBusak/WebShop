using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebShop.DAL.Services.CartServices;
using WebShop.DAL.Services.ProductService;
using WebShop.MVC.ViewModels;

namespace WebShop.MVC.Controllers
{
    public class UserProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;
        public UserProductController(IProductService productService, IMapper mapper, ICartService cartService)
        {
            _productService = productService;
            _mapper = mapper;
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAll();
            var productVMs = _mapper.Map<IEnumerable<ProductResponseVM>>(products);
            return View();
        }
    }
}
