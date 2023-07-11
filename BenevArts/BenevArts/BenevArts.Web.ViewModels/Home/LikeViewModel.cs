using BenevArts.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenevArts.Web.ViewModels.Home
{
	public class LikeViewModel
	{
		public int Id { get; set; }

		public bool IsLikedByCurrentUser { get; set; }

		public Guid AssetId { get; set; }

		public Guid UserID { get; set; }
	}
}
