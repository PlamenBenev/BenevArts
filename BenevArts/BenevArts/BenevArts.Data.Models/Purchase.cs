using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BenevArts.Data.Models
{
    public class Purchase
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

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PurchaseDate { get; set; }

        [Required]
        [Precision(18, 2)]
        [Range(0.00, 10000.00)]
        public decimal Price { get; set; }
    }
}