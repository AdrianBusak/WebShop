﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebShop.DAL.Models;

[Table("Category")]
public partial class Category
{
    [Key]
    public int Id { get; set; }

    [StringLength(256)]
    public string Name { get; set; } = null!;

    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
