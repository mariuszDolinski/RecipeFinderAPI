
namespace RecipeFinderAPI.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RecipeIngridient> Ingridients { get; set; }
        public string Description { get; set; }
    }
}
