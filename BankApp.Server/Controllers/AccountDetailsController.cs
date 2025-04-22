using BankApp.Server.Interfaces;
using BankApp.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountDetailsController : Controller
    {

        private readonly IAccount _accountDetailsService;

        public AccountDetailsController(IAccount accountDetailsService)
        {
            _accountDetailsService = accountDetailsService;
        }

        [HttpGet("account")]
        [Authorize]
        public IActionResult Get() {

            var detailsDTO = _accountDetailsService.GetAccountDetails();

            return Ok(detailsDTO);
        }
       
    }
}
