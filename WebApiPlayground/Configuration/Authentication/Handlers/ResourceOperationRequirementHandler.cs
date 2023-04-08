using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebApiPlayground.Configuration.Authentication.Requirements;
using WebApiPlayground.Entities;

namespace WebApiPlayground.Configuration.Authentication.Handlers
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Restaurant>
    {
        private readonly RestaurantDbContext _dbContext;

        public ResourceOperationRequirementHandler(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ResourceOperationRequirement requirement,
            Restaurant restaurant)
        {
            if (requirement.Operation == ResourceOperation.Create
                || requirement.Operation == ResourceOperation.Read)
            {
                context.Succeed(requirement);
            }
            else
            {
                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (restaurant.CreatedUserId == null || int.Parse(userId) == restaurant.CreatedUserId)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}