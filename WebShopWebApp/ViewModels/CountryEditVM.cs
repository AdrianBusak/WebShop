using System.ComponentModel.DataAnnotations;

namespace WebShopWebApp.ViewModels
{
    public class CountryEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
