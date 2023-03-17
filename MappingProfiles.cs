using AutoMapper;
using RecipeFinderAPI.Entities;
using RecipeFinderAPI.Models;

namespace RecipeFinderAPI
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Ingridient, IngridientDto>();
            CreateMap<CreateRecipeDto, Recipe>();
            CreateMap<Recipe, RecipeDto>();
            CreateMap<UpdateRecipeDto, Recipe>();
        }
    }
}
