
namespace RecipeFinderAPI.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RecipeIngridient> Ingridients { get; set; }
        public string Description { get; set; }
        public bool OnlyForAdults { get; set; } = false;
        
        public int? AuthorId { get; set; }
        public User Author { get; set; }
    }
}
