using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;
using WebShop.DAL.Repositories.CountryRepo;

namespace WebShop.DAL.Services.CountryServices
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepo;

        public CountryService(ICountryRepository repository)
        {
            _countryRepo = repository;
        }

        public List<Country> GetAll() => _countryRepo.GetAll();
        public Country? GetById(int id) => _countryRepo.GetById(id);
        public List<Country> GetForProduct(int productId) => _countryRepo.GetForProduct(productId);
        public void Create(Country country) => _countryRepo.Add(country);
        public void Update(Country country) => _countryRepo.Update(country);
        public void Delete(Country country) => _countryRepo.Delete(country);
    }
}
