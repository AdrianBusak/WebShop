using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;

namespace WebShop.DAL.Services.CountryServices
{
    public interface ICountryService
    {
        List<Country> GetAll();
        Country? GetById(int id);
        List<Country> GetForProduct(int productId);
        void Create(Country country);
        Country? Update(Country country);
        void Delete(Country country);
    }
}
