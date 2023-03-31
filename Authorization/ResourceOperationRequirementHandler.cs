using Microsoft.AspNetCore.Authorization;
using RecipeFinderAPI.Entities;
using System.Security.Claims;

namespace RecipeFinderAPI.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Recipe>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            ResourceOperationRequirement requirement, Recipe recipe)
        {
            if(requirement.ResourceOperation == ResourceOperation.Get ||
                requirement.ResourceOperation == ResourceOperation.GetIngridient ||
                requirement.ResourceOperation == ResourceOperation.Create ||
                requirement.ResourceOperation == ResourceOperation.CreateIngridient)
            {
                context.Succeed(requirement);
            }

            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if(recipe.AuthorId == userId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
