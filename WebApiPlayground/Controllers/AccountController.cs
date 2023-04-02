using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiPlayground.Models.Dtos;
using WebApiPlayground.Services;

namespace WebApiPlayground.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IAccountService _service;

        public AccountController(
            IMapper mapper,
            ILogger<AccountController> logger,
            IAccountService service)
        {
            _mapper = mapper;
            _logger = logger;
            _service = service;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = _service.GenerateJwt(dto);
            return new OkObjectResult(token);
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _service.RegisterUser(dto);
            return new OkResult();
        }
    }
}