using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebShop.API.Models;

[Table("ProductCountry")]
public partial class ProductCountry
{
    public int? CountryId { get; set; }

    public int? ProductId { get; set; }

    [Key]
    public int Id { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("ProductCountries")]
    public virtual Country? Country { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductCountries")]
    public virtual Product? Product { get; set; }
}
