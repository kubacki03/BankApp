using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BankApp.Server.DTO;
using BankApp.Server.Models;
using BankApp.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BankApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;
        private readonly AuthService authService;
        public AuthController(AuthService authService, IConfiguration configuration)
        {
            _config = configuration;
            this.authService = authService;
        }


        [Authorize]
        [HttpGet("private")]
        public IActionResult get()
        {
            return Ok("git");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModelRequest request)
        {


            var token = authService.Login(request);

            if (token == null)
            {
                return Unauthorized();
            }
             


                return Ok(token);
            }


       
        [HttpPost("/register")]
        public IActionResult Register(RegisterModelRequest request)
        {
            var isDone = authService.Register(request);

            if (isDone == false) {
                return Conflict();
            }
            
            return Ok();
        }
    }
}
