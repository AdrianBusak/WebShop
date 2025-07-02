using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebShop.DAL.Models;

[Table("Image")]
public partial class Image
{
    [Key]
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public bool? IsMain { get; set; }

    public int? ProductId { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Images")]
    public virtual Product? Product { get; set; }
}
