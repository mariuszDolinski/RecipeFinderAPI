﻿using System.ComponentModel.DataAnnotations;

namespace RecipeFinderAPI.Models
{
    public class CreateRecipeIngridientDto
    {
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Unit { get; set; }
    }
}
