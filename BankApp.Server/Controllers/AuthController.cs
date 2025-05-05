using BankApp.Server.DTO;
using BankApp.Server.Models;
using BankApp.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthService authService;
        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }

 
        [HttpGet("/login")]
        public IActionResult Login(LoginModelRequest request)
        {

            
            var token = authService.Login(request);


            //if token is null theres no account with that login or password
            if (token == null)
            {
                return NotFound();
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
