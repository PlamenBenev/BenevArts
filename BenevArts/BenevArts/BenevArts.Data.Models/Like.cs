using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BenevArts.Data.Models
{
    public class Like
    {
        [Key]
        public int Id { get; set; }

		[Required]
        public Guid AssetId { get; set; }
        [ForeignKey(nameof(AssetId))]
        public Asset Asset { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;
    }
}