using Microsoft.EntityFrameworkCore;

namespace RecipeFinderAPI.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Ingridient
    {
        public int Id { get; set; }
        public string Name { get; set; }   
    }
}
