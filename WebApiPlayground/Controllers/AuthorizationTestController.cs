using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiPlayground.Configuration.Authentication.Requirements;
using WebApiPlayground.Entities;
using WebApiPlayground.Services;

namespace WebApiPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorizationTestController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public AuthorizationTestController(IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        [HttpGet("allowAnonymous")]
        [AllowAnonymous]
        public ActionResult AllowAnonymous() => Ok();

        [HttpGet("allowForAdminAndManager")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult AllowForAdminAndManager() => Ok();

        [HttpGet("needsAuthorization")]
        public ActionResult NeedsAuthorization() => Ok();

        [HttpGet("useAtLeast20Policy")]
        [Authorize(Policy = "AtLeast20")]
        public ActionResult UseAtLeast20Policy() => Ok();

        [HttpGet("useAuthorizationService/{id}")]
        public async Task<ActionResult> UseAuthorizationService(int id)
        {
            var authorizationResult
             = await _authorizationService.AuthorizeAsync(
                 _userContextService.User,
                 new Restaurant { CreatedUserId = id },
                 new ResourceOperationRequirement(ResourceOperation.Update));

            return Ok(authorizationResult);
        }

        [HttpGet("useHasNationalityPolicy")]
        [Authorize(Policy = "HasNationality")]
        public ActionResult UseHasNationalityPolicy() => Ok();
    }
}