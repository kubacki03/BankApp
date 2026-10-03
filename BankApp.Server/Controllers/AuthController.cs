using BankApp.Server.DTO;
using BankApp.Server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogin _loginService;
        private readonly IRegister _registerService;

        public AuthController(ILogin loginService, IRegister registerService)
        {
            _loginService = loginService;
            _registerService = registerService;
        }

        [Authorize]
        [HttpGet("private")]
        public IActionResult Private()
        {
            return Ok("git");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModelRequest request, CancellationToken cancellationToken)
        {
            var token = await _loginService.LoginAsync(request, cancellationToken);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(RegisterModelRequest request, CancellationToken cancellationToken)
        {
            var isDone = await _registerService.RegisterAsync(request, cancellationToken);

            if (!isDone)
            {
                return Conflict();
            }

            return Ok();
        }
    }
}
