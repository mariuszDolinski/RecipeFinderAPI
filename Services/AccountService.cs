using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RecipeFinderAPI.Entities;
using RecipeFinderAPI.Models;

namespace RecipeFinderAPI.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
    }

    public class AccountService : IAccountService
    {
        private readonly RecipesDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(RecipesDBContext dBContext, IMapper mapper,
            IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dBContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = _mapper.Map<User>(dto);
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, dto.Password);
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }
    }
}
