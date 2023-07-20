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
		public string Name { get; set; } = null!;

		[Required]
		[EmailAddress]
		public string Email { get; set; } = null!;

		[Required]
		[Phone]
		public string Phone { get; set; } = null!;

		[StringLength(1000)]
		public string StoreDescription { get; set; } = null!;

		[Required]
		[ForeignKey(nameof(ApplicationUser))]
		public Guid ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; } = null!;
	}

}
