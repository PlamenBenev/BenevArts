using System.ComponentModel.DataAnnotations;
using BenevArts.Data.Models;
using Microsoft.AspNetCore.Http;

namespace BenevArts.Web.ViewModels.Home
{
    public class EditAssetViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        [Required]
        public IFormFile ZipFile { get; set; } = null!;

        [Required]
        [Display(Name = "Thumbnail")]
        public IFormFile ThumbnailFile { get; set; } = null!;

        [Required]
		[RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = "Only letters, numbers, and spaces are allowed.")]
		[StringLength(1000)]
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

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public IEnumerable<IFormFile> ImagesFiles { get; set; } = new List<IFormFile>();

    }
}
