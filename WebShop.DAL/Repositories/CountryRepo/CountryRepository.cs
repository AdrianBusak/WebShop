using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;

namespace WebShop.DAL.Repositories.CountryRepo
{
    public class CountryRepository : ICountryRepository
    {
        private readonly WebShopContext _context;
        public CountryRepository(WebShopContext context)
        {
            _context = context;
        }

        public List<Country> GetAll() =>
            _context.Countries.ToList();

        public Country? GetById(int id) =>
            _context.Countries.FirstOrDefault(c => c.Id == id);

        public List<Country> GetForProduct(int productId) =>
            _context.ProductCountries
                .Where(pc => pc.ProductId == productId)
                .Include(pc => pc.Country)
                .Select(pc => pc.Country)
                .ToList();

        public void Add(Country country)
        {
            _context.Countries.Add(country);
            _context.SaveChanges();
        }

        public void Update(Country country)
        {
            _context.Countries.Update(country);
            _context.SaveChanges();
        }

        public void Delete(Country country)
        {
            _context.Countries.Remove(country);
            _context.SaveChanges();
        }

    }
}
