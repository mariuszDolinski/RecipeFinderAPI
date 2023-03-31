using FluentValidation;
using RecipeFinderAPI.Entities;

namespace RecipeFinderAPI.Models.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator(RecipesDBContext dbContext) 
        {
            RuleFor(r => r.Password)
                    .MinimumLength(8);
            RuleFor(r => r.ConfirmedPassword)
                .Custom((value, context) =>
                {
                    if(context.InstanceToValidate.Password != value)
                    {
                        context
                        .AddFailure("ConfirmedPassword", "Confirmed password is not the same as password.");
                    }
                });
            RuleFor(r => r.UserName)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    if (dbContext.Users.Any(u => u.UserName == value))
                        context.AddFailure("UserName", "Choosen username is taken.");
                });
            RuleFor(r => r.Email)
                .NotEmpty()
                .EmailAddress()
                .Custom((value, context) =>
                {
                    if (dbContext.Users.Any(u => u.Email == value))
                        context.AddFailure("Email", "That email is taken.");
                });
            RuleFor(r => r.DateOfBirth)
                .NotNull();
        }
    }
}
