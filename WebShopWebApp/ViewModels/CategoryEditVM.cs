using System.ComponentModel.DataAnnotations;

namespace WebShopWebApp.ViewModels
{
    public class CategoryEditVM
    {
        [Required(ErrorMessage = "Category Name is required.")]
        public string Name { get; set; }
    }
}
