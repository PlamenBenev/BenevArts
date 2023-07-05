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
        public string Image { get; set; } = null!;

        [Required]
        public IFormFile ZipFileName { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        //To add binding model
        [Required]
        [Range(typeof(decimal), "0.00", "10000.00", ConvertValueInInvariantCulture = true)]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime UploadDate { get; set; }

        [Required]
        public string Seller { get; set; } = null!;

        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }
}
