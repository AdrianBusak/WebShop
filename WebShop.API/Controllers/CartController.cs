using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop.API.DTOs;
using WebShop.API.Models;

namespace WebShop.API.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly WebShopContext _context;
        private readonly IMapper _mapper;

        public CartController(WebShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/cart/{userId}
        [HttpGet]
        public ActionResult<IEnumerable<CartResponseDto>> GetAll()
        {
            try
            {
                var carts = _context.Carts
                       .Include(c => c.CartItems)
                       .Select(c => _mapper.Map<CartResponseDto>(c))
                       .ToList();

                if (carts == null)
                {
                    return NotFound();
                }

                return Ok(carts);
            }
            catch (Exception)
            {
                return StatusCode(500, "message");
            }
        }
        // GET: api/cart/{userId}
        [HttpGet("{userId}")]
        public ActionResult<CartResponseDto> GetCart(int userId)
        {
            try
            {
                var cart = _context.Carts
                       .Include(c => c.CartItems)
                       .FirstOrDefault(c => c.UserId == userId);

                if (cart == null)
                {
                    return NotFound();
                }

                var cartDto = _mapper.Map<CartResponseDto>(cart);

                return Ok(cartDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "message");
            }
        }

        // POST: api/cart/{userId}/items
        [HttpPost("{userId}/items")]
        public ActionResult AddToCart(int userId, CartItemCreateDto request)
        {
            var product = _context.Products.Find(request.ProductId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            var cart = _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    CartItems = new List<CartItem>()
                };
                _context.Carts.Add(cart);
                _context.SaveChanges();
            }

            var item = cart.CartItems.FirstOrDefault(i => i.ProductId == request.ProductId);
            if (item != null)
            {
                item.Quantity += request.Quantity;
            }
            else
            {
                var cartItem = _mapper.Map<CartItem>(request);
                cartItem.CartId = cart.Id;
                cartItem.UnitPrice = product.Price;

                cart.CartItems.Add(cartItem);
            }

            cart.TotalPrice = cart.CartItems.Sum(i => i.Quantity * i.UnitPrice);
            _context.SaveChanges();

            return NoContent();
        }

        // PUT: api/cart/{userId}/items
        [HttpPut("{userId}/items")]
        public ActionResult UpdateItem(int userId, CartItemUpdateDto request) // doradit
        {
            try
            {
                var cart = _context.Carts
                        .Include(c => c.CartItems)
                        .FirstOrDefault(c => c.UserId == userId);

                if (cart == null)
                {
                    return NotFound();
                }

                var item = cart.CartItems.FirstOrDefault(i => i.ProductId == request.ProductId);
                if (item == null)
                {
                    return NotFound("Item not found in cart.");
                }

                item.Quantity = request.Quantity;

                cart.TotalPrice = cart.CartItems.Sum(i => i.Quantity * i.UnitPrice);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "message");
            }
        }

        // DELETE: api/cart/{userId}/items/{productId}
        [HttpDelete("{userId}/items/{productId}")]
        public ActionResult RemoveItem(int userId, int productId)
        {
            try
            {
                var cart = _context.Carts
                        .Include(c => c.CartItems)
                        .FirstOrDefault(c => c.UserId == userId);

                if (cart == null)
                {
                    return NotFound();
                }

                var item = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
                if (item == null)
                {
                    return NotFound("Item not found in the cart.");
                }
                if(item.Quantity > 1)
                {
                    item.Quantity--;
                }
                else
                {
                    _context.CartItems.Remove(item);
                }

                cart.TotalPrice = cart.CartItems.Sum(i => i.Quantity * i.UnitPrice);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "message");
            }
        }

        // DELETE: api/cart/{userId}
        [HttpDelete("{userId}")]
        public ActionResult ClearCart(int userId)
        {
            try
            {
                var cart = _context.Carts
                        .Include(c => c.CartItems)
                        .FirstOrDefault(c => c.UserId == userId);

                if (cart == null)
                {
                    return NotFound();
                }

                cart.CartItems.Clear();
                cart.TotalPrice = 0;
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "message");
            }
        }
    }

}
