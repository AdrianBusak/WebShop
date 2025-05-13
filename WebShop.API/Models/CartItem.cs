using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebShop.API.Models;

[Keyless]
[Table("CartItem")]
public partial class CartItem
{
    public int? CartId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? UnitPrice { get; set; }

    [ForeignKey("CartId")]
    public virtual Cart? Cart { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product? Product { get; set; }
}
