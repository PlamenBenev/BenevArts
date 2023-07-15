
using Microsoft.AspNetCore.Identity;

namespace BenevArts.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<Like> Likes { get; set; } = new HashSet<Like>();
        public ICollection<Purchase> Purchases { get; set; } = new HashSet<Purchase>();
        public ICollection<Seller> Sellers { get; set; } = new HashSet<Seller>();
        public ICollection<UserFavorites> UserFavorites { get; set; } = new HashSet<UserFavorites>();

    }
}
