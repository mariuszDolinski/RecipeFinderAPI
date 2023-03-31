using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using RecipeFinderAPI.Entities;
using RecipeFinderAPI.Exceptions;
using RecipeFinderAPI.Models;

namespace RecipeFinderAPI.Services
{
    public interface IAdminService
    {
        void ChangeRole(int userId, string role);
        void UpdateRecipeAuthor(int recipeId, int authorId);
        IEnumerable<UserDto> GetAllUsers();
    }
    public class AdminService : IAdminService
    {
        private readonly RecipesDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly AuthenticationSettings _authenticationSettings;

        public AdminService(RecipesDBContext dBContext, IMapper mapper,
            AuthenticationSettings authenticationSettings)
        {
            _dbContext = dBContext;
            _mapper = mapper;
            _authenticationSettings = authenticationSettings;
        }

        public void ChangeRole(int userId, string roleName)
        {
            var role = _dbContext.Roles.FirstOrDefault(r => r.Name == roleName);
            if (role is null)
            {
                throw new NotFoundException($"Role {roleName} doesn't exists");
            }
            var user = GetUserById(userId);

            user.RoleId = role.Id;
            _dbContext.SaveChanges();
        }

        public void UpdateRecipeAuthor(int recipeId, int authorId)
        {
            var recipe = GetRecipeById(recipeId);
            if(!_dbContext.Users.Any(u => u.Id == authorId) )
            {
                throw new NotFoundException("User not found");
            }
            recipe.AuthorId = authorId;
            _dbContext.SaveChanges();;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _dbContext.Users
                .Include(u => u.Role)
                .ToList();
            return _mapper.Map<List<UserDto>>(users);
        }

        private User GetUserById(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user is null)
            {
                throw new NotFoundException($"User not found");
            }
            return user;
        }
        private Recipe GetRecipeById(int recipeId)
        {
            var recipe = _dbContext.Recipes.FirstOrDefault(u => u.Id == recipeId);
            if (recipe is null)
            {
                throw new NotFoundException($"Recipe not found");
            }
            return recipe;
        }
    }
}
