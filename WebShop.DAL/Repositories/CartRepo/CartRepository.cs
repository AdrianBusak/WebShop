﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;

namespace WebShop.DAL.Repositories.CartRepo
{
    public class CartRepository : ICartRepository
    {
        private readonly WebShopContext _context;
        public CartRepository(WebShopContext context)
        {
            _context = context;
        }
        public void Add(Cart cart)
        {
            _context.Carts.Add(cart);
        }

        public void Delete(Cart cart)
        {
            if (cart == null) throw new ArgumentNullException(nameof(cart));
            _context.Carts.Remove(cart);
        }

        public IEnumerable<Cart> GetAll()
        {
            return _context.Carts
                .Include(u => u.User)
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .ToList();
        }

        public Cart? GetByUserId(int userId)
        {
            return _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(p => p.Product)
                    .ThenInclude(i => i.Images)
                .FirstOrDefault(c => c.UserId == userId);
        }

        public void RemoveCartItem(int productId)
        {
            var cartItem = _context.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
            }
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Cart cart)
        {
            _context.Carts.Update(cart);
        }
    }
}
