using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenevArts.Data.Models
{
	public class SellerApplication
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(20)]
		public string StoreName { get; set; } = null!;

		[Required]
		[EmailAddress]
		public string StoreEmail { get; set; } = null!;

		[Phone]
		public string? StorePhone { get; set; }

		[StringLength(1000)]
		public string StoreDescription { get; set; } = null!;

		public string State { get; set; } = null!;

        [Required]
		[ForeignKey(nameof(ApplicationUser))]
		public Guid ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; } = null!;
	}

}
