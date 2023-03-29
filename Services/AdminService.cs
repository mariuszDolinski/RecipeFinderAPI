using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecipeFinderAPI.Entities;
using RecipeFinderAPI.Exceptions;
using RecipeFinderAPI.Models;

namespace RecipeFinderAPI.Services
{
    public interface IAdminService
    {
        void ChangeRole(int userId, string role);
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
    }
}
