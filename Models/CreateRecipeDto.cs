using System.ComponentModel.DataAnnotations;

namespace RecipeFinderAPI.Models
{
    public class CreateRecipeDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool OnlyForAdults { get; set; }
    }
}
