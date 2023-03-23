using AutoMapper;
using RecipeFinderAPI.Entities;
using RecipeFinderAPI.Exceptions;
using RecipeFinderAPI.Models;

namespace RecipeFinderAPI.Services
{
    public interface IUnitService
    {
        IEnumerable<UnitDto> GetAll();
        int CreateUnit(string name);
        UnitDto GetById(int id);
        void RemoveById(int id);
    }
    public class UnitService : IUnitService
    {
        private readonly RecipesDBContext _dbContext;
        private readonly IMapper _mapper;

        public UnitService(RecipesDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        public int CreateUnit(string name)
        {
            if (_dbContext.Units.Any(x => x.Name == name.ToLower()))
                throw new BadRequestException("Unit already exists.");
            var unit = new Unit()
            {
                Name = name.ToLower()
            };
            _dbContext.Units.Add(unit);
            _dbContext.SaveChanges();
            return unit.Id;
        }

        public UnitDto GetById(int id)
        {
            var unit = GetUnitById(id);

            return _mapper.Map<UnitDto>(unit);
        }

        public IEnumerable<UnitDto> GetAll()
        {
            var units = _dbContext.Units.ToList();
            return _mapper.Map<List<UnitDto>>(units);
        }

        public void RemoveById(int id)
        {
            var unit = GetUnitById(id);
            var isRecipeIngridient = _dbContext.RecipeIngridients
                .Any(ri => ri.UnitId == unit.Id);
            if (isRecipeIngridient)
                throw new BadRequestException("Unit cannot be deleted, because some recipe ingridient uses it");
            _dbContext.Units.Remove(unit);
            _dbContext.SaveChanges();
        }

        private Unit GetUnitById(int id)
        {
            var unit = _dbContext.Units.FirstOrDefault(x => x.Id == id);
            if (unit is null)
                throw new NotFoundException("Unit not found.");

            return unit;
        }
    }
}
