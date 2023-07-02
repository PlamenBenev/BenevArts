using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BenevArts.Data.Models
{
    public class Like
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AssetID { get; set; }
        [ForeignKey(nameof(AssetID))]
        public Asset Asset { get; set; } = null!;

        [Required]
        public string UserID { get; set; } = null!;
        [ForeignKey(nameof(UserID))]
        public IdentityUser User { get; set; } = null!;
    }
}