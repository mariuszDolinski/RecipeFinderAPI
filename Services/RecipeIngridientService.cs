using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeFinderAPI.Authorization;
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
        void Update(int recipeId, UpdateRecipeIngridientDto dto, int ingridientId);
        void Delete(int recipeId);
        void DeleteById(int recipeId, int ingridientId);
    }
    public class RecipeIngridientService : IRecipeIngridientService
    {
        private readonly RecipesDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;

        public RecipeIngridientService(RecipesDBContext dBContext, IMapper mapper, 
            IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _dbContext = dBContext;
            _mapper = mapper;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
        }

        public int Create(int recipeId, CreateRecipeIngridientDto dto)
        {
            var recipe = GetRecipeById(recipeId);
            
            if (recipe.AuthorId != _userContextService.GetUserId)
            {
                throw new ForbidException("Operation forbidden.");
            }
            var ingridientId = GetIngridientId(dto.Name);
            var unitId = GetUnitId(dto.Unit);

            if (dto.Amount <= 0)
                throw new BadRequestException("Amount is not correct number.");
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

        public void Update(int recipeId, UpdateRecipeIngridientDto dto, int ingridientId)
        {
            var recipe = GetRecipeById(recipeId);
            var ingridient = recipe.Ingridients.FirstOrDefault(ing => ing.Id == ingridientId);
            if (ingridient is null)
                throw new NotFoundException("Ingridient not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User,
               recipe, new ResourceOperationRequirement(ResourceOperation.UpdateIngridient)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("User not authorized for updating recipe ingridient.");
            }

            var newIngridientId = GetIngridientId(dto.Name);
            var newUnitId = GetUnitId(dto.Unit);
            
            if (dto.Amount <= 0)
                throw new BadRequestException("Amount is not correct number.");
            ingridient.Amount = dto.Amount;
            ingridient.Description = dto.Description;
            ingridient.IngridientId = newIngridientId;
            ingridient.UnitId = newUnitId;
            _dbContext.SaveChanges();
        }

        public void Delete(int recipeId)
        {
            var recipe = GetRecipeById(recipeId);

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User,
               recipe, new ResourceOperationRequirement(ResourceOperation.DeleteIngridient)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("User not authorized for deleting recipe ingridients.");
            }

            _dbContext.RemoveRange(recipe.Ingridients);
            _dbContext.SaveChanges();
        }

        public void DeleteById(int recipeId, int ingridientId)
        {
            var recipe = GetRecipeById(recipeId);
            var ingridient = _dbContext.RecipeIngridients.FirstOrDefault(ing => ing.Id == ingridientId);
            if (ingridient is null || ingridient.RecipeId != recipeId)
                throw new NotFoundException("Ingridient not found.");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User,
               recipe, new ResourceOperationRequirement(ResourceOperation.UpdateIngridient)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("User not authorized for deleting recipe ingridient.");
            }

            _dbContext.Remove(ingridient);
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
                throw new NotFoundException("Ingridient name  is required.");

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
                throw new NotFoundException("Unit name is required.");

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
