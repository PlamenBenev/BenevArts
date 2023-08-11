using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;


namespace BenevArts.Web.ViewModels.Home
{
	public class BaseAssetViewModel
	{
		[Required(ErrorMessage = "Required field!")]
		[StringLength(100, ErrorMessage = "The input is too long!")]
		[RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = "Only letters, numbers, and spaces are allowed.")]
		public string Title { get; set; } = null!;

		[Required(ErrorMessage = "Required field!")]
		public IFormFile ZipFileName { get; set; } = null!;

		[Required(ErrorMessage = "Required field!")]
		public IFormFile Thumbnail { get; set; } = null!;

		[Required(ErrorMessage = "Required field!")]
		[StringLength(1000, ErrorMessage = "The input is too long!")]
		[RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = "Only letters, numbers, and spaces are allowed.")]
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
		[Required(ErrorMessage = "Required field!")]
		[Range(typeof(decimal), "0.00", "10000.00", ConvertValueInInvariantCulture = true)]
		public decimal Price { get; set; }

		public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
		public IEnumerable<IFormFile> Images { get; set; } = new List<IFormFile>();
	}
}
