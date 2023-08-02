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
		[RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = "Only letters, numbers, and spaces are allowed.")]
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