using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace BenevArts.Infrastructure.Model
{
    [Comment("Product Table")]
    public class Product
    {
        [Comment("Product Id")]
        public int Id { get; set; }

        [Comment("Product Name")]
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = null!;

        [Comment("Product Price")]
        [Required]
        public decimal Price { get; set; }
    }
}
