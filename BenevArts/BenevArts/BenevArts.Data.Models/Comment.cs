using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BenevArts.Data.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10000)]
        public string Content { get; set; } = null!;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PostedDate { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        public Guid AssetId { get; set; }
        [ForeignKey(nameof(AssetId))]
        public Asset Asset { get; set; } = null!;
    }
}