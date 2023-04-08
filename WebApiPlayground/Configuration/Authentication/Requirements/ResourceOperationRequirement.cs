using Microsoft.AspNetCore.Authorization;

namespace WebApiPlayground.Configuration.Authentication.Requirements
{
    public enum ResourceOperation
    {
        Create,
        Read,
        Update,
        Delete,
    }

    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperationRequirement(ResourceOperation operation)
        {
            Operation = operation;
        }

        public ResourceOperation Operation { get; }
    }
}