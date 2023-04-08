using Microsoft.AspNetCore.Authorization;

namespace WebApiPlayground.Configuration.Authentication.Requirements
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public MinimumAgeRequirement(int age)
        {
            MinimumAge = age;
        }

        public int MinimumAge { get; }
    }
}