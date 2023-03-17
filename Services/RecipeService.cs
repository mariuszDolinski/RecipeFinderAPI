using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeFinderAPI.Entities;
using RecipeFinderAPI.Exceptions;
using RecipeFinderAPI.Models;

namespace RecipeFinderAPI.Services
{
    public interface IRecipeService
    {
        int CreateRecipe(CreateRecipeDto dto);
        IEnumerable<RecipeDto> GetAll();
        RecipeDto GetById(int id);
        void UpdateRecipe(UpdateRecipeDto dto, int id);
    }

    public class RecipeService : IRecipeService
    {
        private readonly RecipesDBContext _dbContext;
        private readonly IMapper _mapper;

        public RecipeService(RecipesDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        public int CreateRecipe(CreateRecipeDto dto)
        {
            var recipe = _mapper.Map<Recipe>(dto);
            _dbContext.Recipes.Add(recipe);
            _dbContext.SaveChanges();

            return recipe.Id;
        }

        public IEnumerable<RecipeDto> GetAll()
        {
            var recipes = _dbContext.Recipes
                .Include(r => r.Ingridients)
                .ToList();
            return _mapper.Map<List<RecipeDto>>(recipes);
        }

        public RecipeDto GetById(int id)
        {
            var recipe = _dbContext.Recipes
                .Include(r => r.Ingridients)
                .FirstOrDefault(r => r.Id == id);

            if (recipe is null)
                throw new NotFoundException("Recipe not found.");

            return _mapper.Map<RecipeDto>(recipe);
        }

        public void UpdateRecipe(UpdateRecipeDto dto, int id)
        {
            var recipe = _dbContext.Recipes
                .FirstOrDefault(r => r.Id == id);

            if (recipe is null)
                throw new NotFoundException("Recipe not found");

            recipe.Name = dto.Name;
            recipe.Description = dto.Description;
            _dbContext.SaveChanges();
        }
    }
}
