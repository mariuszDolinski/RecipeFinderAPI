using RecipeFinderAPI.Entities;

namespace RecipeFinderAPI.Seeders
{
    public class RecipeSeeder
    {
        private RecipesDBContext _dbContext;
        public RecipeSeeder(RecipesDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void ClearData()
        {
            _dbContext.Ingridients.RemoveRange(_dbContext.Ingridients);
            _dbContext.Units.RemoveRange(_dbContext.Units);
            _dbContext.SaveChanges();
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                //seeding ingridients with values define in enum
                if (!_dbContext.Ingridients.Any())
                {
                    var ingridients = GetIngridients();
                    _dbContext.Ingridients.AddRange(ingridients);
                    _dbContext.SaveChanges();
                }
                //seeding unitss with values define in enum
                if (!_dbContext.Units.Any())
                {
                    var units = GetUnits();
                    _dbContext.Units.AddRange(units);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Ingridient> GetIngridients()
        {
            var ingridients = new List<Ingridient>();
            string replaceString;
            foreach(var ing in Enum.GetNames(typeof(IngridientsEnum)))
            {
                if (ing.Contains('_'))
                    replaceString = ing.Replace('_', ' ');
                else
                    replaceString = ing;
                ingridients.Add(new Ingridient() { Name = replaceString });
            }

            return ingridients;
        }

        private IEnumerable<Unit> GetUnits()
        {
            var units = new List<Unit>();
            foreach (var unit in Enum.GetNames(typeof(UnitsEnum)))
            {
                units.Add(new Unit() { Name = unit });
            }

            return units;
        }
    }
}
