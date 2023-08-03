using System.ComponentModel.DataAnnotations;

namespace BenevArts.Web.ViewModels.Home
{
	public class SellerApplicationViewModel
	{
		public int Id { get; set; }

        //[Required]
        //[StringLength(20)]
        //public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Required field!")]
        [StringLength(20, ErrorMessage = "The input is too long!")]
        public string StoreName { get; set; } = null!;


        [Required(ErrorMessage = "Required field!")]
        [EmailAddress(ErrorMessage = "Invalid Email!")]
		public string Email { get; set; } = null!;

		[Phone(ErrorMessage = "Invalid Phone Number")]
		public string? Phone { get; set; }

        [Required(ErrorMessage = "Required field!")]
        [StringLength(1000,ErrorMessage = "The input is too long!")]
		[RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = "Only letters, numbers, and spaces are allowed.")]
		public string StoreDescription { get; set; } = null!;

		public string State { get; set; } = "Pending";

		public Guid ApplicationUserId { get; set; }
	}
}
