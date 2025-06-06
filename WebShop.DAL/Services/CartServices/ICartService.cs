using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;

namespace WebShop.DAL.Services.CartServices
{
    public interface ICartService
    {
        IEnumerable<Cart> GetAll();
        Cart? GetByUserId(int userId);
        Cart Create(Cart cart);
        void AddToCart(int userId, int productId, int quantity);
        void UpdateItem(int userId, int productId, int quantity);
        void RemoveItem(int userId, int productId);
        void ClearCart(int userId);
    }
}
