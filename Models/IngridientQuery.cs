namespace RecipeFinderAPI.Models
{
    public class IngridientQuery
    {
        public string Search { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
