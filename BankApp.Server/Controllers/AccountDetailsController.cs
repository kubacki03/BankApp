using BankApp.Server.Interfaces;
using BankApp.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("/account")]
        [Authorize]
        public IActionResult Get() {

            var detailsDTO = _accountDetailsService.GetAccountDetails();

            return Ok(detailsDTO);
        }

        [Authorize]
        [HttpGet("/lastTransfers")]
        public IActionResult GetLastTransfers([FromBody] string iban) {
            var username = User.Identity?.Name;
            var user = _accountDetailsService.DoesUserExistByPesel(username);
            if (user == null)
            {
                return Unauthorized();
            }
            var transfers = _accountDetailsService.GetLastTransferList(iban);

            return Ok(transfers);
        }
    }
}
