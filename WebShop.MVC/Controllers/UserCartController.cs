using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.DAL.Services.CartServices;
using WebShop.DAL.Services.UserServices;
using WebShop.MVC.ViewModels;

namespace WebShop.MVC.Controllers
{
    [Authorize(Roles = "user")]
    public class UserCartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserCartController(
            ICartService cartService,
            IUserService userService,
            IMapper mapper)
        {
            _cartService = cartService;
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var username = User.Identity?.Name;
            if (username == null)
            {
                return RedirectToAction("Login", "User");
            }
            var user = _userService.GetUserByUsername(username);

            var cart = _cartService.GetByUserId(user.Id);

            if (cart == null)
            {
                return View();
            }

            var cartVm = _mapper.Map<CartVM>(cart);

            return View(cartVm);
        }

        [HttpPost]
        public IActionResult AddToCart([FromBody] CartItemVM cartItemVM)
        {
            var username = User.Identity?.Name;
            if (username == null)
            {
                return RedirectToAction("Login", "User");
            }
            var user = _userService.GetUserByUsername(username);


            if (username == null)
            {
                return RedirectToAction("Login", "User");
            }
            _cartService.AddToCart(user.Id, cartItemVM.ProductId, cartItemVM.Quantity);
            return RedirectToAction("Index", "UserProduct");
        }
        [HttpPost]
        public IActionResult UpdateCartItem(int productId, int quantity)
        {
            var username = User.Identity?.Name;
            if (username == null)
            {
                return RedirectToAction("Login", "User");
            }
            var user = _userService.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound();
            }
            _cartService.UpdateItem(user.Id, productId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var username = User.Identity?.Name;
            if (username == null)
            {
                return RedirectToAction("Login", "User");
            }
            var user = _userService.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound();
            }
            _cartService.RemoveItem(user.Id, productId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult ClearCart()
        {
            var username = User.Identity?.Name;
            if (username == null)
            {
                return RedirectToAction("Login", "User");
            }
            var user = _userService.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound();
            }
            _cartService.ClearCart(user.Id);
            return RedirectToAction("Index");
        }
    }
}
