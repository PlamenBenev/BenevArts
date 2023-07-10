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
		[StringLength(10000)]
		public string Content { get; set; } = null!;

		[Required]
		[DataType(DataType.DateTime)]
		public DateTime PostedDate { get; set; }

		[Required]
		public string User { get; set; } = null!;

		[Required]
		public Guid AssetId { get; set; }
	}
}
