using Microsoft.AspNetCore.Authorization;
using RecipeFinderAPI.Entities;
using System.Security.Claims;

namespace RecipeFinderAPI.Authorization
{
    public class AdultsOnlyRequirementHandler : AuthorizationHandler<AdultsOnlyRequirement, Recipe>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            AdultsOnlyRequirement requirement, Recipe recipe)
        {
            var dateOfBirth = DateTime.Parse(context.User.FindFirst(c => c.Type == "DateOfBirth").Value);

            if (!recipe.OnlyForAdults)
            {
                context.Succeed(requirement);
            }
            else 
            {
                if(dateOfBirth.AddYears(18) <= DateTime.Now)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
