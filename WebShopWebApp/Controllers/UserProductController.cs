using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop.DAL.Models;
using WebShop.DAL.Services.CartServices;
using WebShop.DAL.Services.ProductService;
using WebShop.DAL.Services.UserServices;
using WebShopWebApp.ViewModels;

namespace WebShopWebApp.Controllers
{
    [Authorize(Roles = "user")]
    public class UserProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserProductController(
            IProductService productService,
            IMapper mapper,
            IConfiguration configuration)
        {
            _productService = productService;
            _mapper = mapper;
            _configuration = configuration;
        }

        public IActionResult Index(SearchVM searchVm)
        {

            IQueryable<Product> products = _productService.GetAll().AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchVm.Q))
            {
                products = products.Where(p => p.Name.Contains(searchVm.Q));
            }

            var totalItems = products.Count();

            switch (searchVm.OrderBy?.ToLower())
            {
                case "name": products = products.OrderBy(p => p.Name); break;
                case "price": products = products.OrderBy(p => p.Price); break;
                case "brand": products = products.OrderBy(p => p.Brand); break;
                case "category": products = products.OrderBy(p => p.Category.Name); break;
                default: products = products.OrderBy(p => p.Id); break;
            }

            products = products.Skip((searchVm.Page - 1) * searchVm.Size).Take(searchVm.Size);
            searchVm.Products = _mapper.Map<List<ProductUserVM>>(products.ToList());

            int expand = _configuration.GetValue("Paging:ExpandPages", 2);
            searchVm.LastPage = (int)Math.Ceiling((double)totalItems / searchVm.Size);
            searchVm.FromPager = Math.Max(1, searchVm.Page - expand);
            searchVm.ToPager = Math.Min(searchVm.LastPage, searchVm.Page + expand);

            return View(searchVm);
        }

        public IActionResult Details(int id)
        {
            var product = _productService.GetWithDetails(id);
            if (product == null)
                return NotFound();

            var productVM = _mapper.Map<ProductDetailsVM>(product);
            return View(productVM);
        }
    }
}
