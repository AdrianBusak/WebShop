using System.ComponentModel.DataAnnotations;

namespace WebShopWebApp.ViewModels
{
    public class CategoryEditVM
    {
        [Required(ErrorMessage = "Category Name is required.")]
        [Display(Name = "Category")]
        public string Name { get; set; }
    }
}
