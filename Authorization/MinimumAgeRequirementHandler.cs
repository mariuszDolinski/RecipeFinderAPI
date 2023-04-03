using Microsoft.AspNetCore.Authorization;

namespace RecipeFinderAPI.Authorization
{
    public class MinimumAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var dateOfBirth = DateTime.Parse(context.User.FindFirst(c => c.Type == "DateOfBirth").Value);

            if(dateOfBirth.AddYears(requirement.MinimumAge) <= DateTime.Now)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
