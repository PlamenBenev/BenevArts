using BenevArts.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<UserFavorites> UserFavorites { get; set; }
        public DbSet<SellerApplication> SellersApplications { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Comment>()
                .HasOne(c => c.Asset)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AssetId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Like>()
                .HasOne(l => l.Asset)
                .WithMany(a => a.Likes)
                .HasForeignKey(l => l.AssetId)
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

            builder.Entity<Asset>()
                .HasMany(s => s.Comments)
                .WithOne(a => a.Asset)
                .HasForeignKey(a => a.AssetId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Seller>()
	            .HasMany(a => a.Assets)
	            .WithOne(s => s.Seller)
	            .HasForeignKey(a => a.SellerId)
	            .OnDelete(DeleteBehavior.NoAction);

			var categories = new[]
            {
               new Category { Id = 1, Name = "Aircraft", Image = "https://i.pinimg.com/originals/0b/82/60/0b8260c6a692955a75d048cf12e20164.jpg"},
               new Category { Id = 2, Name = "Animals",  Image = "https://360view.hum3d.com/zoom/Animals/Allosaurus_1000_0001.jpg"},
               new Category { Id = 3, Name = "Architectural", Image = "https://www.renderhub.com/zyed/container-office-building/container-office-building.jpg" },
               new Category { Id = 4, Name = "Exterior" , Image = "https://www.renderhub.com/mm2endra/complete-house-exterior/complete-house-exterior.jpg"},
               new Category { Id = 5, Name = "Interior" , Image = "https://mir-s3-cdn-cf.behance.net/project_modules/1400/4eddca30402501.56210b527c788.jpg"},
               new Category { Id = 6, Name = "Car" , Image = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fget.wallhere.com%2Fphoto%2F1500x1500-px-GT1-LE-lemans-mans-Nissan-R390-race-racing-supercar-1672607.jpg&f=1&nofb=1&ipt=62e16a3e4f3cfcf3ab999db9b598e17e6ce3a49cea0cbf88e375afbbba3206c1&ipo=images"},
               new Category { Id = 7, Name = "Character" , Image = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fstatic.zerochan.net%2FHrothgar.full.2600742.png&f=1&nofb=1&ipt=f36ae45a8caf6f2980beb52a695e6db13dc4edc2340d9793110d24f36794f80d&ipo=images"},
               new Category { Id = 8, Name = "Food" , Image = "https://static.turbosquid.com/Preview/2019/07/17__13_46_54/Signature.jpgD810065D-0F62-4EBB-847C-AA57E3F7D50ADefault.jpg"},
               new Category { Id = 9, Name = "Furniture" , Image = "https://static.turbosquid.com/Preview/2020/07/20__00_13_48/1f.pngFE53EBFF-5100-497F-8874-49F155AC925BDefault.jpg"},
               new Category { Id = 10, Name = "Household" , Image = "https://www.renderhub.com/bsw2142/kitchen-appliances/kitchen-appliances.jpg"},
               new Category { Id = 11, Name = "Industrial" , Image = "https://img2.cgtrader.com/items/1949905/2aef8f1d69/loom-machine-3d-model-max-fbx.jpg"},
               new Category { Id = 12, Name = "Plant" , Image = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.pinimg.com%2F736x%2Fee%2Fd1%2F33%2Feed133e60e4646d749c0a7e87da7f9e8.jpg&f=1&nofb=1&ipt=bc48b05a520757ee97c69e1e8c9f79e06ee8eae82203792e334bf3556edef1d1&ipo=images"},
               new Category { Id = 13, Name = "Space" , Image = "https://cdn.shopify.com/s/files/1/2191/8173/products/discovery-space-shuttle-nasa-3d-models-_3_1024x1024.jpg?v=1504007478"},
               new Category { Id = 14, Name = "Vehicle" , Image = "https://www.renderhub.com/sky3dstudios69/construction-vehicle-001/construction-vehicle-001.jpg"},
               new Category { Id = 15, Name = "Watercraft" , Image = "https://3dlenta.com/components/com_virtuemart/shop_image/product/Watercraft_003_4ab8a73fa534b.jpg"},
               new Category { Id = 16, Name = "Military" , Image = "https://static.turbosquid.com/Preview/2014/05/16__03_52_30/MATV_Grad_Cam01.jpg217fdd9e-8a2c-49ba-9048-aeb9a1311120Zoom.jpg"}
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