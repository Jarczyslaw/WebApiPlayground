using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApiPlayground.Configuration
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
            var dateOfBirth = DateTime.Parse(context.User.FindFirst(x => x.Type == "DateOfBirth").Value);
            var email = context.User.FindFirst(x => x.Type == ClaimTypes.Email).Value;

            if (dateOfBirth.AddYears(requirement.MinimumAge) < DateTime.Today)
            {
                _logger.LogInformation($"Authorization succeded for user with email: {email}");
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}