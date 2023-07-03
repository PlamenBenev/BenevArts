using System.ComponentModel.DataAnnotations;

namespace BenevArts.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        public ICollection<Asset> Assets { get; set; } = new HashSet<Asset>();
    }
}
