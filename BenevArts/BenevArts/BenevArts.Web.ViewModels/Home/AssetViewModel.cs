using System.ComponentModel.DataAnnotations.Schema;

namespace BenevArts.Web.ViewModels.Home
{
    public class AssetViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public string ZipFileName { get; set; } = null!;
        public decimal Price { get; set; }
        public string Category { get; set; } = null!;
        public int CategoryId { get; set; }
        public string Seller { get; set; } = null!;
        public DateTime UploadDate { get; set; }

        public bool CGIModel { get; set; }
        public bool Textures { get; set; }
        public bool Materials { get; set; }
        public bool Animated { get; set; }
        public bool Rigged { get; set; }
        public bool LowPoly { get; set; }
        public bool PBR { get; set; }
        public bool UVUnwrapped { get; set; }

		public bool IsLikedByCurrentUser { get; set; }
		public bool IsFavoritedByCurrentUser { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public IEnumerable<LikeViewModel> Likes { get; set; } = new List<LikeViewModel>();
		public IEnumerable<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
		public IEnumerable<string> Images { get; set; } = new List<string>();

	}
}
