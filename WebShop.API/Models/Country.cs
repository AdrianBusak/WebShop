using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebShop.API.Models;

[Table("Country")]
public partial class Country
{
    [Key]
    public int Id { get; set; }

    [StringLength(256)]
    public string Name { get; set; } = null!;

    [InverseProperty("Country")]
    public virtual ICollection<ProductCountry> ProductCountries { get; set; } = new List<ProductCountry>();
}
