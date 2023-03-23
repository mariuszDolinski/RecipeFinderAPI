namespace RecipeFinderAPI.Entities
{
    public class RecipeIngridient
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int IngridientId { get; set; }
        public int UnitId { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
