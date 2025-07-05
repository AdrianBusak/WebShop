using System.ComponentModel.DataAnnotations;

namespace WebShopWebApp.ViewModels
{
    public class CategoryCreateVM
    {
        [Required(ErrorMessage = "Category Name is required.")]
        public string Name { get; set; }
    }
}
