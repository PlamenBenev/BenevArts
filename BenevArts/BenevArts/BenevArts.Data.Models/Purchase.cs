using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BenevArts.Data.Models
{
    public class Purchase
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

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PurchaseDate { get; set; }

        [Required]
        [Precision(18, 2)]
        [Range(0.00, 10.00)]
        public decimal Price { get; set; }
    }
}