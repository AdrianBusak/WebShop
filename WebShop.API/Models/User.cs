using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebShop.API.Models;

[Table("User")]
public partial class User
{
    [Key]
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    [StringLength(50)]
    public string Username { get; set; } = null!;

    [StringLength(256)]
    public string FirstName { get; set; } = null!;

    [StringLength(256)]
    public string LastName { get; set; } = null!;

    [StringLength(256)]
    public string Email { get; set; } = null!;

    [StringLength(256)]
    public string PwdHash { get; set; } = null!;

    [StringLength(256)]
    public string PwdSalt { get; set; } = null!;

    [StringLength(256)]
    public string? Phone { get; set; }

    public int RoleId { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; } = null!;
}
