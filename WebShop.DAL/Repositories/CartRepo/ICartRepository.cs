using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;

namespace WebShop.DAL.Repositories.CartRepo
{
    public interface ICartRepository
    {
        IEnumerable<Cart> GetAll();
        Cart? GetByUserId(int userId);
        void Add(Cart cart);
        void Update(Cart cart);
        void Delete(Cart cart);
        void SaveChanges();
    }
}
