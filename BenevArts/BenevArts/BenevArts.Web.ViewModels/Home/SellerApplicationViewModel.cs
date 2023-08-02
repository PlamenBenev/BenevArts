using System.ComponentModel.DataAnnotations;

namespace BenevArts.Web.ViewModels.Home
{
	public class SellerApplicationViewModel
	{
		public int Id { get; set; }

		[Required]
		[StringLength(20)]
		[RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = "Only letters, numbers, and spaces are allowed.")]
		public string UserName { get; set; } = null!;

        [Required]
        [StringLength(20)]
		[RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = "Only letters, numbers, and spaces are allowed.")]
		public string StoreName { get; set; } = null!;


        [Required]
		[EmailAddress]
		public string Email { get; set; } = null!;

		[Phone]
		[RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers are allowed.")]
		public string? Phone { get; set; }

		[Required]
		[StringLength(1000)]
		[RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = "Only letters, numbers, and spaces are allowed.")]
		public string StoreDescription { get; set; } = null!;

		public string State { get; set; } = "Pending";

		public Guid ApplicationUserId { get; set; }
	}
}
