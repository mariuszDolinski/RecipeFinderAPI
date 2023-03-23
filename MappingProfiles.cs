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
            CreateMap<Unit, UnitDto>();
            CreateMap<CreateRecipeDto, Recipe>();
            CreateMap<Recipe, RecipeDto>();
            CreateMap<RecipeIngridient, RecipeIngridientDto>();
            CreateMap<RecipeIngridientDto, RecipeIngridient>();
            CreateMap<UpdateRecipeDto, Recipe>();
            CreateMap<CreateRecipeIngridientDto, RecipeIngridient>();
        }
    }
}
