﻿using RecipeFinderAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace RecipeFinderAPI.Models
{
    public class RecipeDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<RecipeIngridientDto> Ingridients { get; set; }
    }
}
