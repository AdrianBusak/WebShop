using System.ComponentModel.DataAnnotations;

namespace WebShop.MVC.ViewModels
{
    public class CountryCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
