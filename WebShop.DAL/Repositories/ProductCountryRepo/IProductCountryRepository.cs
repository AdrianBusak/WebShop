using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;

namespace WebShop.DAL.Repositories.ProductCountryRepo
{
    public interface IProductCountryRepository
    {
        bool Exists(int productId, int countryId);
        void Add(ProductCountry productCountry);
        void Remove(ProductCountry productCountry);
        ProductCountry? Get(int productId, int countryId);
    }
}
