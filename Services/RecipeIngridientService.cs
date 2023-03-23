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
            var ingridient = _dbContext
                .Ingridients
                .FirstOrDefault(r => r.Name == name);

            if (ingridient is null)
                throw new NotFoundException("Ingridient name not found.");

            return ingridient.Id;
        }
        private int GetUnitId(string name)
        {
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
