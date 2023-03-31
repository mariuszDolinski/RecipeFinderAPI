using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipeFinderAPI.Entities;
using RecipeFinderAPI.Models;
using RecipeFinderAPI.Exceptions;

namespace RecipeFinderAPI.Services
{
    public interface IIngridientService
    {
        PaginationResult<IngridientDto> GetAll(IngridientQuery query);
        int CreateIngridient(string name);
        IngridientDto GetById(int id);
        void RemoveById(int id);
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
        public PaginationResult<IngridientDto> GetAll(IngridientQuery query)
        {
            var baseQuery = _dbContext
                .Ingridients
                .Where(ing => query.Search == null || ing.Name.Contains(query.Search));

            var ingridients = baseQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToList();
          
            var ingridientDtos = _mapper.Map<List<IngridientDto>>(ingridients);

            var result =  new PaginationResult<IngridientDto>(ingridientDtos, baseQuery.Count(),
                query.PageSize, query.PageNumber);

            if(baseQuery.Count() > 0 && query.PageNumber > result.TotalPages)
            {
                throw new BadRequestException("PageNumber is to big.");
            }

            return result;
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
        public void RemoveById(int id)
        {
            var ingridient = GetIngridientById(id);
            var isRecipeIngridient = _dbContext.RecipeIngridients
                .Any(ri => ri.IngridientId == ingridient.Id);
            if (isRecipeIngridient)
                throw new BadRequestException("Ingridient cannot be deleted, because some recipe ingridient uses it.");
            _dbContext.Ingridients.Remove(ingridient);
            _dbContext.SaveChanges();
        }

        private Ingridient GetIngridientById(int id)
        {
            var ingridient = _dbContext.Ingridients.FirstOrDefault(x => x.Id == id);
            if (ingridient is null)
                throw new NotFoundException("Ingridient not found.");

            return ingridient;
        }
    }
}
