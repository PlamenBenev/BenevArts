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
        public string Image { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        //To add binding model
        [Required]
        [Precision(18, 2)]
        [Range(typeof(decimal), "0.00", "10000.00", ConvertValueInInvariantCulture = true)]
        public decimal Price { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime UploadDate { get; set; }

        // Navigation properties
        [Required]
        [ForeignKey(nameof(Seller))]
        public Guid SellerId { get; set; }

        public Seller Seller { get; set; } = null!;

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<Like> Likes { get; set; } = new HashSet<Like>();
        public ICollection<Purchase> Purchases { get; set; } = new HashSet<Purchase>();

    }
}