﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RecipeFinderAPI.Authorization;
using RecipeFinderAPI.Entities;
using RecipeFinderAPI.Exceptions;
using RecipeFinderAPI.Models;
using System.Net.NetworkInformation;

namespace RecipeFinderAPI.Services
{
    public interface IRecipeService
    {
        int CreateRecipe(CreateRecipeDto dto);
        IEnumerable<RecipeDto> GetAll();
        RecipeDto GetById(int id);
        IEnumerable<RecipeDto> GetByIngridient(FindRecipesByIngridientsDto dto, int mode);
        void UpdateRecipe(UpdateRecipeDto dto, int id);
        void RemoveById(int id);
    }

    public class RecipeService : IRecipeService
    {
        private readonly RecipesDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;

        public RecipeService(RecipesDBContext dBContext, IMapper mapper, 
            IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _dbContext = dBContext;
            _mapper = mapper;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
        }

        public int CreateRecipe(CreateRecipeDto dto)
        {
            var recipe = _mapper.Map<Recipe>(dto);
            recipe.AuthorId = _userContextService.GetUserId;
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
            var recipe = GetRecipeById(id);

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User,
                recipe, new AdultsOnlyRequirement()).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("User is to young to see this recipe.");
            }

            return _mapper.Map<RecipeDto>(recipe);
        }
        public void UpdateRecipe(UpdateRecipeDto dto, int id)
        {
            var recipe = GetRecipeById(id);

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User,
                recipe, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("User not authorized for updating recipe.");
            }

            recipe.Name = dto.Name;
            if(dto.Description is not null)
                recipe.Description = dto.Description;
            _dbContext.SaveChanges();
        }
        /// <summary>
        /// Find all recipe by given string list of ingridients and mode value
        /// </summary>
        /// <param name="dto">list of ingridients names as strings</param>
        /// <param name="mode">if 0 (default) recipes that have all given ingridients will be searched
        /// , if 1 recipes with all ingridients only from given list will be searched</param>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        public IEnumerable<RecipeDto> GetByIngridient(FindRecipesByIngridientsDto dto, int mode)
        {
            if (dto is null || dto.Ingridients.IsNullOrEmpty())
            {
                throw new BadRequestException("Invalid format of data");
            }

            var recipes = _dbContext.Recipes.
                Include(r => r.Ingridients).ToList();
            var result = new List<RecipeDto>();
            int count;
            int modeCount;
            foreach (var recipe in recipes)
            {
                if (recipe.Ingridients.IsNullOrEmpty())
                {
                    continue;
                }
                count = 0;
                foreach(var ing in recipe.Ingridients)
                {
                    var name = _dbContext.Ingridients.FirstOrDefault(i => i.Id == ing.IngridientId).Name;
                    if(dto.Ingridients.Contains(name))
                    {
                        count++;
                    }
                }
                modeCount = (mode == 0) ? dto.Ingridients.Count : recipe.Ingridients.Count;
                if (count == modeCount)
                {
                    result.Add(_mapper.Map<RecipeDto>(recipe));
                }
            }

            return result;
        }
        public void RemoveById(int id)
        {
            var recipe = GetRecipeById(id);

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User,
                recipe, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("User not authorized for deleting recipe.");
            }

            _dbContext.Recipes.Remove(recipe);
            _dbContext.SaveChanges();
        }

        private Recipe GetRecipeById(int id)
        {
            var recipe = _dbContext.Recipes
                .Include(r => r.Ingridients)
                .FirstOrDefault(r => r.Id == id);

            if (recipe is null)
                throw new NotFoundException("Recipe not found.");

            return recipe;
        }
    }
}
