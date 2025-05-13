using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebShop.API.Models;

[Keyless]
[Table("ProductCountry")]
public partial class ProductCountry
{
    public int? CountryId { get; set; }

    public int? ProductId { get; set; }

    [ForeignKey("CountryId")]
    public virtual Country? Country { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product? Product { get; set; }
}
