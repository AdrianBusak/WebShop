using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.DAL.Models;
using WebShop.DAL.Services.CartServices;
using WebShop.DAL.Services.LogServices;
using WebShop.DAL.Services.UserServices;
using WebShopWebApp.ViewModels;

namespace WebShopWebApp.Controllers
{
    [Authorize(Roles = "user")]
    public class UserCartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogService _logger;

        public UserCartController(
            ICartService cartService,
            IUserService userService,
            IMapper mapper,
            ILogService logger
            )
        {
            _cartService = cartService;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
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
            _logger.Add(
                new Log { 
                    Message = $"User {username} added product {cartItemVM.ProductId} to cart with quantity {cartItemVM.Quantity}",
                    Level = "Info",
                });
            return RedirectToAction("Index", "UserProduct");
        }
        [HttpPut]
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
            _logger.Add(
                new Log
                {
                    Message = $"User {username} updated product {productId} in cart to quantity {quantity}",
                    Level = "Info",
                });
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
            _logger.Add(
                new Log
                {
                    Message = $"User {username} removed product {productId} from cart",
                    Level = "Info",
                });
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
            _logger.Add(
                new Log
                {
                    Message = $"User {username} cleared their cart",
                    Level = "Info",
                });
            return RedirectToAction("Index");
        }
    }
}
