using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        public string ZipFileName { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        public int CategoryId { get; set; }

        [Required]
        [Precision(18, 2)] //To add binding model
        [Range(typeof(decimal), "0.00", "10000.00", ConvertValueInInvariantCulture = true)]
        public decimal Price { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime UploadDate { get; set; }

        // Technical Details
        public bool CGIModel { get; set; }
        public bool Textures { get; set; }
        public bool Materials { get; set; }
        public bool Animated { get; set; }
        public bool Rigged { get; set; }
        public bool LowPoly { get; set; }
        public bool PBR { get; set; }
        public bool UVUnwrapped { get; set; }

        // Navigation properties
        [Required]
        [ForeignKey(nameof(Seller))]
        public Guid SellerId { get; set; }

        public Seller Seller { get; set; } = null!;

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<Like> Likes { get; set; } = new HashSet<Like>();
        public ICollection<Purchase> Purchases { get; set; } = new HashSet<Purchase>();
        public ICollection<AssetImage> Images { get; set; } = new List<AssetImage>();

    }
}