using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BenevArts.Data.Models
{
    public class Like
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid AssetID { get; set; }
        [ForeignKey(nameof(AssetID))]
        public Asset Asset { get; set; } = null!;

        [Required]
        public string UserID { get; set; } = null!;
        [ForeignKey(nameof(UserID))]
        public IdentityUser User { get; set; } = null!;
    }
}