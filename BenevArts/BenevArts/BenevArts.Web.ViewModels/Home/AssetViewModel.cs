

namespace BenevArts.Web.ViewModels.Home
{
    public class AssetViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ZipFileName { get; set; } = null!;
        public decimal Price { get; set; }
        public string Category { get; set; } = null!;
        public string SellerName { get; set; } = null!;
        public DateTime UploadDate { get; set; }

        public bool CGIModel { get; set; }
        public bool Textures { get; set; }
        public bool Materials { get; set; }
        public bool Animated { get; set; }
        public bool Rigged { get; set; }
        public bool LowPoly { get; set; }
        public bool PBR { get; set; }
        public bool UVUnwrapped { get; set; }
    }
}
