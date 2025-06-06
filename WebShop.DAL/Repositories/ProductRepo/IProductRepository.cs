using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;

namespace WebShop.DAL.Repositories.ProductRepo
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetByCategoryId(int categoryId);
        Product? GetById(int id);
        IEnumerable<Product> GetInCountry(int countryId);
        Product? GetWithDetails(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        void Save();
    }
}
