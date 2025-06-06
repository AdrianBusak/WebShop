using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop.API.DTOs;
using WebShop.DAL.Models;
using WebShop.DAL.Services.CartServices;

namespace WebShop.API.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        // GET: api/cart/{userId}
        [HttpGet]
        public ActionResult<IEnumerable<CartResponseDto>> GetAll()
        {
            try
            {
                var carts = _cartService.GetAll()
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
                var cart = _cartService.GetByUserId(userId);

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
            try
            {
                _cartService.AddToCart(userId, request.ProductId, request.Quantity);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "message");
            }
        }

        // PUT: api/cart/{userId}/items
        [HttpPut("{userId}/items")]
        public ActionResult UpdateItem(int userId, CartItemUpdateDto request) // doradit
        {
            try
            {
                _cartService.UpdateItem(userId, request.ProductId, request.Quantity);

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
                _cartService.RemoveItem(userId, productId);

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
                _cartService.ClearCart(userId);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "message");
            }
        }
    }

}
