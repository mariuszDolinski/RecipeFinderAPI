using Microsoft.AspNetCore.Authorization;

namespace RecipeFinderAPI.Authorization
{
    public enum ResourceOperation
    {
        Create,
        CreateIngridient,
        Update,
        UpdateIngridient,
        Get,
        GetIngridient,
        Delete,
        DeleteIngridient
    }
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperation ResourceOperation { get; }
        public ResourceOperationRequirement(ResourceOperation operation)
        {
            ResourceOperation = operation;
        }
    }
}
