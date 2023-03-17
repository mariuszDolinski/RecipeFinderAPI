﻿using System.ComponentModel.DataAnnotations;

namespace RecipeFinderAPI.Models
{
    public class IngridientDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
