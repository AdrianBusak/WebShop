using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;
using WebShop.DAL.Repositories.CategoryRepo;

namespace WebShop.DAL.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
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

            _repository.Delete(category);
            _repository.SaveChanges();
            return category;
        }
    }
}

