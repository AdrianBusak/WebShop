using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;

namespace WebShop.DAL.Repositories.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly WebShopContext _context;

        public UserRepository(WebShopContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _context.Users
                .Include(u => u.Carts)
                .FirstOrDefault(x => x.Id == id);


            if (item != null)
            {
                _context.Carts.RemoveRange(item.Carts);
                _context.Users.Remove(item);

                _context.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users
                .Include(u => u.Role)
                .ToList();
        }

        public User? GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User? GetByUsername(string username)
        {
            return _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Username == username);
        }

        public void Update(User user)
        {
            if (user != null)
            {
                _context.Update(user);
                _context.SaveChanges();
            }
        }
    }
}

