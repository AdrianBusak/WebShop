using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;

namespace WebShop.DAL.Services.CategoryServices
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category? GetById(int id);
        Category Create(Category category);
        Category? Update(int id, Category updatedCategory);
        Category? Delete(int id);
    }
}
