using System.ComponentModel.DataAnnotations;

namespace WebShopWebApp.ViewModels
{
    public class CountryCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
