﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebShop.DAL.Models;

public partial class WebShopContext : DbContext
{
    public WebShopContext()
    {
    }

    public WebShopContext(DbContextOptions<WebShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCountry> ProductCountries { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Default");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart__3214EC075320B34A");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.Carts).HasConstraintName("FK__Cart__UserId__5BE2A6F2");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CartItem__3214EC072C4A81B6");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems).HasConstraintName("FK__CartItem__CartId__5DCAEF64");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems).HasConstraintName("FK__CartItem__Produc__5EBF139D");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC07BDCD114F");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Country__3214EC0771E2CEBD");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Image__3214EC077466F786");

            entity.HasOne(d => d.Product).WithMany(p => p.Images).HasConstraintName("FK__Image__ProductId__5535A963");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Log__3214EC07A71BB778");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC076958F014");

            entity.HasOne(d => d.Category).WithMany(p => p.Products).HasConstraintName("FK__Product__Categor__52593CB8");
        });

        modelBuilder.Entity<ProductCountry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductC__3214EC070AAE5503");

            entity.HasOne(d => d.Country).WithMany(p => p.ProductCountries).HasConstraintName("FK__ProductCo__Count__571DF1D5");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductCountries).HasConstraintName("FK__ProductCo__Produ__5812160E");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC072627BA0A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07FB97A308");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__RoleId__4BAC3F29");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
