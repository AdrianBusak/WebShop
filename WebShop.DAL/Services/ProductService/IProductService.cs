using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;

namespace WebShop.DAL.Services.ProductService
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetByCategoryId(int categoryId);
        Product? GetById(int id);
        IEnumerable<Product> GetInCountry(int countryId);
        Product? GetWithDetails(int id);
        void Create(Product product);
        void Update(Product product);
        void Delete(Product product);
    }
}
