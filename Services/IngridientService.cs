using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipeFinderAPI.Entities;
using RecipeFinderAPI.Models;
using RecipeFinderAPI.Exceptions;

namespace RecipeFinderAPI.Services
{
    public interface IIngridientService
    {
        IEnumerable<IngridientDto> GetAll();
        int CreateIngridient(string name);
        IngridientDto GetById(int id);
    }
    public class IngridientService : IIngridientService
    {
        private readonly RecipesDBContext _dbContext;
        private readonly IMapper _mapper;

        public IngridientService(RecipesDBContext dBContext, IMapper mapper) 
        { 
            _dbContext = dBContext;
            _mapper = mapper;
        }

        public int CreateIngridient(string name)
        {
            if (_dbContext.Ingridients.Any(x => x.Name == name.ToLower()))
                throw new BadRequestException("Ingridient already exists.");
            var ingridient = new Ingridient()
            {
                Name = name.ToLower()
            };
            _dbContext.Ingridients.Add(ingridient);
            _dbContext.SaveChanges();
            return ingridient.Id;
        }

        public IEnumerable<IngridientDto> GetAll()
        {
            var ingridients = _dbContext.Ingridients.ToList();
            return _mapper.Map<List<IngridientDto>>(ingridients);
        }

        public IngridientDto GetById(int id)
        {
            var ingridient = _dbContext
                .Ingridients
                .FirstOrDefault(i => i.Id == id);

            if (ingridient is null)
                throw new NotFoundException("Ingridient not found.");

            return _mapper.Map<IngridientDto>(ingridient);
        }
    }
}
