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
		[RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = "Only letters, numbers, and spaces are allowed.")]
		public string StoreName { get; set; } = null!;

		[Required]
		[EmailAddress]
		public string StoreEmail { get; set; } = null!;

		[Phone]
		[RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers are allowed.")]
		public string? StorePhone { get; set; }

		[StringLength(1000)]
		[RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = "Only letters, numbers, and spaces are allowed.")]
		public string StoreDescription { get; set; } = null!;

		[RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = "Only letters, numbers, and spaces are allowed.")]
		public string State { get; set; } = null!;

        [Required]
		[ForeignKey(nameof(ApplicationUser))]
		public Guid ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; } = null!;
	}

}
