namespace RecipeFinderAPI.Models
{
    public class CreateRecipeIngridientDto
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
    }
}
