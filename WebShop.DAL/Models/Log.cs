using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebShop.DAL.Models;

[Table("Log")]
public partial class Log
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Timestamp { get; set; }

    [StringLength(50)]
    public string? Level { get; set; }

    public string? Message { get; set; }
}
