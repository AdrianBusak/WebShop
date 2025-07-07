using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;
using WebShop.DAL.Repositories.CartRepo;
using WebShop.DAL.Repositories.ImageRepo;
using WebShop.DAL.Repositories.ProductCountryRepo;
using WebShop.DAL.Repositories.ProductRepo;

namespace WebShop.DAL.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly IImageRepository _imageRepo;
        private readonly ICartRepository _cartRepo;
        private readonly IProductCountryRepository _productCountryRepo;
        public ProductService(IProductRepository productRepository,
            IImageRepository imageRepository,
            ICartRepository cartRepo,
            IProductCountryRepository countryRepository
            )
        {
            _productRepo = productRepository;
            _imageRepo = imageRepository;
            _cartRepo = cartRepo;
            _productCountryRepo = countryRepository;
        }

        public IEnumerable<Product> GetAll() => _productRepo.GetAll();

        public IEnumerable<Product> GetByCategoryId(int categoryId) => _productRepo.GetByCategoryId(categoryId);

        public Product? GetById(int id) => _productRepo.GetById(id);

        public IEnumerable<Product> GetInCountry(int countryId) => _productRepo.GetInCountry(countryId);

        public Product? GetWithDetails(int id) => _productRepo.GetWithDetails(id);

        public void Create(Product product)
        {
            _productRepo.Add(product);
            _productRepo.Save();
        }

        public void Update(Product product)
        {
            _productRepo.Update(product);
            _productRepo.Save();
        }

        public void Delete(Product product)
        {
            _cartRepo.RemoveCartItem(product.Id);

            _imageRepo.DeleteRange(product.Images);

            _productCountryRepo.RemoveRange(product.ProductCountries);

            _productRepo.Delete(product);
            _productRepo.Save();
        }
    }
}

