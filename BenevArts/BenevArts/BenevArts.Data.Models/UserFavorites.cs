using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenevArts.Data.Models
{
    public class UserFavorites
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(Asset))]
        public Guid AssetId { get; set; }
        public Asset Asset { get; set; } = null!;
    }
}
