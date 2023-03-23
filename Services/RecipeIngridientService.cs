using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeFinderAPI.Entities;
using RecipeFinderAPI.Exceptions;
using RecipeFinderAPI.Models;

namespace RecipeFinderAPI.Services
{ 
    public interface IRecipeIngridientService
    {
        int Create(int recipeId, CreateRecipeIngridientDto dto);
        IEnumerable<RecipeIngridientDto> GetAll(int recipeId);
        RecipeIngridientDto GetById(int recipeId, int ingridientId);
        void Update(int recipeId, CreateRecipeIngridientDto dto, int ingridientId);
    }
    public class RecipeIngridientService : IRecipeIngridientService
    {
        private readonly RecipesDBContext _dbContext;
        private readonly IMapper _mapper;

        public RecipeIngridientService(RecipesDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        public int Create(int recipeId, CreateRecipeIngridientDto dto)
        {
            var ingridientId = GetIngridientId(dto.Name);
            var unitId = GetUnitId(dto.Unit);
            GetRecipeById(recipeId);

            var ingridientEntity = _mapper.Map<RecipeIngridient>(dto);
            ingridientEntity.RecipeId = recipeId;
            ingridientEntity.IngridientId = ingridientId;
            ingridientEntity.UnitId = unitId;
            _dbContext.Add(ingridientEntity);
            _dbContext.SaveChanges();

            return ingridientEntity.Id;
        }

        public IEnumerable<RecipeIngridientDto> GetAll(int recipeId)
        {
            var recipe = GetRecipeById(recipeId);

            return _mapper.Map<List<RecipeIngridientDto>>(recipe.Ingridients);
        }
        public RecipeIngridientDto GetById(int recipeId, int ingridientId)
        {
            var recipe = GetRecipeById(recipeId);
            var ingridient = recipe.Ingridients.FirstOrDefault(ing => ing.Id == ingridientId);

            if (ingridient is null)
                throw new NotFoundException("Recipe ingridient not found.");

            return _mapper.Map<RecipeIngridientDto>(ingridient);
        }

        public void Update(int recipeId, CreateRecipeIngridientDto dto, int ingridientId)
        {
            var recipe = GetRecipeById(recipeId);
            var ingridient = recipe.Ingridients.FirstOrDefault(ing => ing.Id == ingridientId);
            if (ingridient is null)
                throw new NotFoundException("Ingridient not found");

            var newIngridientId = GetIngridientId(dto.Name);
            var newUnitId = GetUnitId(dto.Unit);

            if(!string.IsNullOrEmpty(dto.Description))
                ingridient.Description = dto.Description;
            if (dto.Amount <= 0)
                throw new BadRequestException("Amount is not correct number.");
            ingridient.Amount = dto.Amount;
            if (newIngridientId != 0)
                ingridient.IngridientId = newIngridientId;
            if (newUnitId !=0)
                ingridient.UnitId = newUnitId;
            _dbContext.SaveChanges();
        }

        #region private methods
        private Recipe GetRecipeById(int id)
        {
            var recipe = _dbContext.Recipes
                .Include(r => r.Ingridients)
                .FirstOrDefault(r => r.Id == id);

            if (recipe is null)
                throw new NotFoundException("Recipe not found.");

            return recipe;
        }
        private int GetIngridientId(string name)
        {
            if (string.IsNullOrEmpty(name))
                return default;

            var ingridient = _dbContext
                .Ingridients
                .FirstOrDefault(r => r.Name == name);

            if (ingridient is null)
                throw new NotFoundException("Ingridient name not found.");

            return ingridient.Id;
        }
        private int GetUnitId(string name)
        {
            if (string.IsNullOrEmpty(name))
                return default;

            var unit = _dbContext
                .Units
                .FirstOrDefault(r => r.Name == name);

            if (unit is null)
                throw new NotFoundException("Unit name not found.");

            return unit.Id;
        }
        #endregion
    }
}
