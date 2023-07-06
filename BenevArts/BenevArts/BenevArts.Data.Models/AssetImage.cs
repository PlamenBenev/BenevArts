using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenevArts.Data.Models
{
    public class AssetImage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string ImageName { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Asset))]
        public Guid AssetId { get; set; }
        public Asset Asset { get; set; } = null!;
    }
}
