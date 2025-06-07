using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;

namespace WebShop.DAL.Repositories.ProductCountryRepo
{
    public class ProductCountryRepository : IProductCountryRepository
    {
        private readonly WebShopContext _context;

        public ProductCountryRepository(WebShopContext context)
        {
            _context = context;
        }

        public bool Exists(int productId, int countryId)
        {
            return _context.ProductCountries.Any(pc => pc.ProductId == productId && pc.CountryId == countryId);
        }

        public ProductCountry? Get(int productId, int countryId)
        {
            return _context.ProductCountries.FirstOrDefault(pc => pc.ProductId == productId && pc.CountryId == countryId);
        }

        public void Add(ProductCountry productCountry)
        {
            _context.ProductCountries.Add(productCountry);
            _context.SaveChanges();
        }

        public void Remove(ProductCountry productCountry)
        {
            _context.ProductCountries.Remove(productCountry);
            _context.SaveChanges();
        }

    }
}

