using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BenevArts.Data.Models
{
    public class Asset
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Category { get; set; } = null!;


        //To fix it
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime UploadDate { get; set; }

        [Required]
        public string UploadedByUserID { get; set; } = null!;
        // Navigation properties
        [ForeignKey(nameof(UploadedByUserID))]
        public IdentityUser UploadedByUser { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    }
}