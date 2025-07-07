using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;

namespace WebShop.DAL.Repositories.ProductRepo
{
    public class ProductRepository : IProductRepository
    {
        private readonly WebShopContext _context;
        public ProductRepository(WebShopContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetAll() =>
            _context.Products
            .Include(p => p.Category)
            .Include(p => p.Images)
            .Include(p => p.ProductCountries)
            .ThenInclude(pc => pc.Country)
            .ToList();

        public IEnumerable<Product> GetByCategoryId(int categoryId) =>
            _context.Products
            .Where(p => p.CategoryId == categoryId)
            .ToList();

        public Product? GetById(int id) =>
            _context.Products
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Include(p => p.ProductCountries)
                    .ThenInclude(pc => pc.Country)
                .FirstOrDefault(p => p.Id == id);

        public IEnumerable<Product?> GetInCountry(int countryId) =>
            _context.ProductCountries
                .Include(pc => pc.Product)
                .Where(pc => pc.CountryId == countryId)
                .Select(pc => pc.Product)
                .ToList();

        public Product? GetWithDetails(int id) =>
            _context.Products
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Include(p => p.ProductCountries)
                    .ThenInclude(pc => pc.Country)
                .FirstOrDefault(p => p.Id == id);
            
        public void Add(Product product) =>
            _context.Products.Add(product);

        public void Update(Product product) =>
            _context.Products.Update(product);

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public void Save() =>
            _context.SaveChanges();
    }
}
