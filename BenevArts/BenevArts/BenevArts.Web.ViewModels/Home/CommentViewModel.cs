using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenevArts.Web.ViewModels.Home
{
	public class CommentViewModel
	{
		public int Id { get; set; }

		[Required]
		[StringLength(1000)]
		[RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = "Only letters, numbers, and spaces are allowed.")]
		public string Content { get; set; } = null!;

		[DataType(DataType.DateTime)]
		public DateTime PostedDate { get; set; }

		public string User { get; set; } = null!;

		public Guid AssetId { get; set; }
		public Guid UserId { get; set; }
	}
}
