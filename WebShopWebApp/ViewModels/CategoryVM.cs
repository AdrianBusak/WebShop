using System.ComponentModel.DataAnnotations;

namespace WebShopWebApp.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category Name is required.")]
        public string Name { get; set; }
    }
}
