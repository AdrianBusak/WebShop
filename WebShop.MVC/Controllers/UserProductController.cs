using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebShop.DAL.Services.CartServices;
using WebShop.DAL.Services.ProductService;
using WebShop.DAL.Services.UserServices;
using WebShop.MVC.ViewModels;

namespace WebShop.MVC.Controllers
{
    public class UserProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public UserProductController(
            IProductService productService,
            IMapper mapper
            )
        {
            _productService = productService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAll();
            var productVMs = _mapper.Map<List<ProductUserVM>>(products);
            return View(productVMs);
        }

        public IActionResult Details(int id)
        {
            var product = _productService.GetWithDetails(id);
            if (product == null)
            {
                return NotFound();
            }
            var productVM = _mapper.Map<ProductDetailsVM>(product);
            return View(productVM);
        }

        //[HttpGet]
        //public IActionResult Search(string query)
        //{
        //    if (string.IsNullOrEmpty(query))
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    var products = _productService.SearchProducts(query);
        //    var productVMs = _mapper.Map<IEnumerable<ProductResponseVM>>(products);
        //    return View("Index", productVMs);
        //}
    }
}
