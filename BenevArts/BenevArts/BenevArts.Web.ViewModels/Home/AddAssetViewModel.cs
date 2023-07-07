using System.ComponentModel.DataAnnotations;
using BenevArts.Data.Models;
using Microsoft.AspNetCore.Http;

namespace BenevArts.Web.ViewModels.Home
{
    public class AddAssetViewModel
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        [Required]
        public IFormFile ZipFileName { get; set; } = null!;

        [Required]
        public IFormFile Thumbnail { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public bool CGIModel { get; set; }
        public bool Textures { get; set; }
        public bool Materials { get; set; }
        public bool Animated { get; set; }
        public bool Rigged { get; set; }
        public bool LowPoly { get; set; }
        public bool PBR { get; set; }
        public bool UVUnwrapped { get; set; }

        [Required]
        public int CategoryId { get; set; }

        //To add binding model
        [Required]
        [Range(typeof(decimal), "0.00", "10000.00", ConvertValueInInvariantCulture = true)]
        public decimal Price { get; set; }

        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<IFormFile> Images { get; set; } = new List<IFormFile>();

    }
}
