using System.ComponentModel.DataAnnotations;

namespace WebShop.MVC.ViewModels
{
    public class CountryVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}