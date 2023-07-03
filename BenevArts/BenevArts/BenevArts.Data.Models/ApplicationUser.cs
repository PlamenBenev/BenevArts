
using Microsoft.AspNetCore.Identity;

namespace BenevArts.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

    }
}
