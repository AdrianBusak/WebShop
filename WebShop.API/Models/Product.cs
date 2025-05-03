using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebShop.API.Models;

[Table("Product")]
public partial class Product
{
    [Key]
    public int Id { get; set; }

    [StringLength(256)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    public int? CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category? Category { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}
