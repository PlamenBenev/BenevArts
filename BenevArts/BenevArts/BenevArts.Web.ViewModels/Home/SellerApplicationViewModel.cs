using System.ComponentModel.DataAnnotations;

namespace BenevArts.Web.ViewModels.Home
{
	public class SellerApplicationViewModel
	{
		public int Id { get; set; }

		[Required]
		[StringLength(20)]
		public string UserName { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string StoreName { get; set; } = null!;


        [Required]
		[EmailAddress]
		public string Email { get; set; } = null!;

		[Phone]
		public string? Phone { get; set; }

		[Required]
		[StringLength(1000)]
		public string StoreDescription { get; set; } = null!;

		public string State { get; set; } = "Pending";

		public Guid ApplicationUserId { get; set; }
	}
}
