
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenevArts.Data.Models
{
    public class Seller
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        
		public ICollection<Asset> Assets { get; set; } = new HashSet<Asset>();
    }
}
