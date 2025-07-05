using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;
using WebShop.DAL.Repositories.CartRepo;
using WebShop.DAL.Repositories.ProductRepo;

namespace WebShop.DAL.Services.CartServices
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }
        public void AddToCart(int userId, int productId, int quantity)
        {
            var product = _productRepository.GetById(productId)
               ?? throw new Exception("Product not found.");

            var cart = _cartRepository.GetByUserId(userId);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    CartItems = new List<CartItem>()
                };
                _cartRepository.Add(cart);
                _cartRepository.SaveChanges();
            }

            var item = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = product.Price
                });
            }

            cart.TotalPrice = cart.CartItems.Sum(i => i.Quantity * i.UnitPrice);
            _cartRepository.SaveChanges();
        }

        public void ClearCart(int userId)
        {
            var cart = _cartRepository.GetByUserId(userId);
            if (cart != null)
            {
                cart.CartItems.Clear();
                cart.TotalPrice = 0;
                _cartRepository.Update(cart);
                _cartRepository.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Cart not found for the specified user.");
            }
        }

        public Cart Create(Cart cart)
        {
            if (cart == null)
            {
                throw new ArgumentNullException(nameof(cart), "Cart cannot be null.");
            }
            _cartRepository.Add(cart);
            _cartRepository.SaveChanges();
            return cart;
        }

        public IEnumerable<Cart> GetAll()
        {
            var carts = _cartRepository.GetAll();
            if (carts == null || !carts.Any())
            {
                carts = new List<Cart>();
            }

            return carts;
        }

        public Cart? GetByUserId(int userId)
        {
            return _cartRepository.GetByUserId(userId);
        }

        public void RemoveItem(int userId, int productId)
        {
            var cart = _cartRepository.GetByUserId(userId);
            if (cart == null)
            {
                throw new ArgumentException("Cart not found for the specified user.");
            }
            var itemToRemove = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);

            if (itemToRemove == null)
            {
                throw new ArgumentException("Item not found in the cart.");
            }

            cart.CartItems.Remove(itemToRemove);

            _cartRepository.SaveChanges();

        }

        public void UpdateItem(int userId, int productId, int quantity)
        {
            var cart = _cartRepository.GetByUserId(userId);
            if (cart == null)
            {
                throw new ArgumentException("Cart not found for the specified user.");
            }
            var itemToUpdate = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity = quantity;
                _cartRepository.Update(cart);
                _cartRepository.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Item not found in the cart.");
            }
        }
    }
}
