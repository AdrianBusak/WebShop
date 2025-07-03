using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.DAL.Services.CartServices;
using WebShop.MVC.ViewModels;

namespace WebShop.MVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminCartController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;
        public AdminCartController(IMapper mapper, ICartService cartService)
        {
            _mapper = mapper;
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var carts = _cartService.GetAll();

            var cartViewModels = _mapper.Map<IEnumerable<UserCartListVM>>(carts);
            return View(cartViewModels);

        }
    }
}
