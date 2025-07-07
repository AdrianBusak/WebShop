using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;
using WebShop.DAL.Repositories.CategoryRepo;
using WebShop.DAL.Services.ProductService;

namespace WebShop.DAL.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IProductService _productService;

        public CategoryService(ICategoryRepository repository, IProductService productService)
        {
            _repository = repository;
            _productService = productService;
        }

        public IEnumerable<Category> GetAll()
        {
            return _repository.GetAll();
        }

        public Category? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Category Create(Category category)
        {
            bool exists = _repository.GetAll().Any(c => c.Name == category.Name);
            if (exists)
            {
                throw new ArgumentException("Category already exists.");
            }

            _repository.Add(category);
            _repository.SaveChanges();
            return category;
        }


        public Category? Update(int id, Category updatedCategory)
        {
            var existing = _repository.GetById(id);
            if (existing == null) return null;

            existing.Name = updatedCategory.Name;
            _repository.Update(existing);
            _repository.SaveChanges();
            return existing;
        }

        public Category? Delete(int id)
        {
            var category = _repository.GetById(id);
            if (category == null) return null;

            IEnumerable<Product> products = _productService.GetByCategoryId(id);

            products.ToList()
                .ForEach(p => p.CategoryId = null);

            _repository.Delete(category);
            _repository.SaveChanges();
            return category;
        }
    }
}

