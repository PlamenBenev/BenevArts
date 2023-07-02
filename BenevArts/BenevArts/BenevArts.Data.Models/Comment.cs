using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BenevArts.Data.Models
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Content { get; set; } = null!;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PostedDate { get; set; }

        [Required]
        public string UserID { get; set; } = null!;

        [ForeignKey(nameof(UserID))]
        public IdentityUser User { get; set; } = null!;

        [Required]
        public Guid AssetID { get; set; }
        [ForeignKey(nameof(AssetID))]
        public Asset Asset { get; set; } = null!;
    }
}