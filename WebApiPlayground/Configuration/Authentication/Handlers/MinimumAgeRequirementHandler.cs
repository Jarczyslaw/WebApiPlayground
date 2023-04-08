using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebApiPlayground.Configuration.Authentication.Requirements;

namespace WebApiPlayground.Configuration.Authentication.Handlers
{
    public class MinimumAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        private readonly ILogger<MinimumAgeRequirementHandler> _logger;

        public MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var dateOfBirth = DateTime.Parse(context.User.FindFirstValue("DateOfBirth"));
            var name = context.User.FindFirstValue(ClaimTypes.Name);

            if (dateOfBirth.AddYears(requirement.MinimumAge) < DateTime.Today)
            {
                _logger.LogInformation($"Authorization succeded for user with email: {name}");
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}