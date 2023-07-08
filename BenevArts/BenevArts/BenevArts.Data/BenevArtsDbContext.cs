using BenevArts.Data.Models;
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
        public DbSet<AssetImage> AssetImages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Comment>()
                .HasOne(c => c.Asset)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AssetID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Like>()
                .HasOne(l => l.Asset)
                .WithMany(a => a.Likes)
                .HasForeignKey(l => l.AssetID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Purchase>()
                .HasOne(p => p.Asset)
                .WithMany(a => a.Purchases)
                .HasForeignKey(p => p.AssetID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Asset>()
                .HasOne(a => a.Seller)
                .WithMany(s => s.Assets)
                .HasForeignKey(a => a.SellerId)
                .OnDelete(DeleteBehavior.NoAction);

			//builder.Entity<Asset>()
	  //          .HasMany(s => s.Comments)
	  //          .WithOne(a => a.Asset)
	  //          .HasForeignKey(a => a.AssetID)
	  //          .OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Seller>()
	            .HasMany(a => a.Assets)
	            .WithOne(s => s.Seller)
	            .HasForeignKey(a => a.SellerId)
	            .OnDelete(DeleteBehavior.NoAction);

			var categories = new[]
            {
               new Category { Id = 1, Name = "Aircraft" },
               new Category { Id = 2, Name = "Animals" },
               new Category { Id = 3, Name = "Architectural" },
               new Category { Id = 4, Name = "Exterior" },
               new Category { Id = 5, Name = "Interior" },
               new Category { Id = 6, Name = "Car" },
               new Category { Id = 7, Name = "Character" },
               new Category { Id = 8, Name = "Food" },
               new Category { Id = 9, Name = "Furniture" },
               new Category { Id = 10, Name = "Household" },
               new Category { Id = 11, Name = "Industrial" },
               new Category { Id = 12, Name = "Plant" },
               new Category { Id = 13, Name = "Space" },
               new Category { Id = 14, Name = "Vehicle" },
               new Category { Id = 15, Name = "Watercraft" },
               new Category { Id = 16, Name = "Military" }
            };

            foreach (var category in categories)
            {
                builder.Entity<Category>()
                    .HasData(category);
            }


            base.OnModelCreating(builder);
        }
    }
}