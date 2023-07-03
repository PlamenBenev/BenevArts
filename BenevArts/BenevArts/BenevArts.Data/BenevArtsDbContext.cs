﻿using BenevArts.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BenevArts.Data
{
    public class BenevArtsDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public BenevArtsDbContext(DbContextOptions<BenevArtsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Comment>()
                .HasOne(c => c.Asset)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AssetID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Like>()
                .HasOne(l => l.Asset)
                .WithMany(l => l.Likes)
                .HasForeignKey(l => l.AssetID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Purchase>()
                .HasOne(p => p.Asset)
                .WithMany(p => p.Purchases)
                .HasForeignKey(p => p.AssetID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Seller>()
                .HasOne(s => s.User)
                .WithMany(u => u.Sellers)
                .HasForeignKey(s => s.UserId);

            base.OnModelCreating(builder);
        }
    }
}