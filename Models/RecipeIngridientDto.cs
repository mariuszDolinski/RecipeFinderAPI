using System.ComponentModel.DataAnnotations;

namespace RecipeFinderAPI.Models
{
    public class RecipeIngridientDto
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
