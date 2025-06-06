using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;

namespace WebShop.DAL.Repositories.CountryRepo
{
    public interface ICountryRepository
    {
        List<Country> GetAll();
        Country? GetById(int id);
        List<Country> GetForProduct(int productId);
        void Add(Country country);
        void Update(Country country);
        void Delete(Country country);
    }
}
