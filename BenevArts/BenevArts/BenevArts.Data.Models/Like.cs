﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BenevArts.Data.Models
{
    public class Like
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid AssetID { get; set; }
        [ForeignKey(nameof(AssetID))]
        public Asset Asset { get; set; } = null!;

        [Required]
        public Guid UserID { get; set; }
        [ForeignKey(nameof(UserID))]
        public ApplicationUser User { get; set; } = null!;
    }
}