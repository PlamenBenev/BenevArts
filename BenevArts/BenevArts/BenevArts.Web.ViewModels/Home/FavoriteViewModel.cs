using BenevArts.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenevArts.Web.ViewModels.Home
{
    public class FavoriteViewModel
    {
        public Guid UserId { get; set; }

        public Guid AssetId { get; set; }
    }
}
