using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;
using WebShop.DAL.Repositories.ProductCountryRepo;

namespace WebShop.DAL.Services.ProductCountryServices
{
    public class ProductCountryService : IProductCountryService
    {
        private readonly IProductCountryRepository _productCountryRepository;
        public ProductCountryService(IProductCountryRepository productCountryRepository)
        {
            _productCountryRepository = productCountryRepository;
        }

        public ProductCountry? Get(int productId, int countryId)
        {
            return _productCountryRepository.Get(productId, countryId);
        }

        public void Link(int productId, int countryId)
        {
            if (_productCountryRepository.Exists(productId, countryId))
                throw new Exception($"Product with ID {productId} is already linked to country with ID {countryId}.");

            var productCountry = new ProductCountry
            {
                ProductId = productId,
                CountryId = countryId
            };

            _productCountryRepository.Add(productCountry);
        }

        public void Unlink(int productId, int countryId)
        {
            var productCountry = _productCountryRepository.Get(productId, countryId);
            if (productCountry == null)
                throw new Exception($"Product with ID {productId} is not linked to country with ID {countryId}.");

            _productCountryRepository.Remove(productCountry);
        }

        public bool Exists(int productId, int countryId)
        {
            return _productCountryRepository.Exists(productId, countryId);
        }
    }
}
