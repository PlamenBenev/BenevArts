using System.ComponentModel.DataAnnotations;

namespace BenevArts.Web.ViewModels.Home
{
	public class SellerApplicationViewModel
	{
		public int Id { get; set; }

		[StringLength(20)]
		public string Name { get; set; } = null!;

		[EmailAddress]
		public string Email { get; set; } = null!;

		[Phone]
		public string Phone { get; set; } = null!;

		[StringLength(1000)]
		public string StoreDescription { get; set; } = null!;

		public string State { get; set; } = "Pending";

		public Guid ApplicationUserId { get; set; }
	}
}
