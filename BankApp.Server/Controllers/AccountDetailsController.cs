using System.Security.Claims;
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
        public IConfiguration _config;
        public AccountDetailsController(IAccount accountDetailsService, IConfiguration configuration)
        {
            _config = configuration;
            _accountDetailsService = accountDetailsService;
        }

        [HttpGet("account")]
        [Authorize]
        public IActionResult Get() {
            var email = User.Identity?.Name;
            var detailsDTO = _accountDetailsService.GetAccountDetails(email);
            
            return Ok(detailsDTO);
        }

        [Authorize]
        [HttpGet("lastTransfers")]
        public IActionResult GetLastTransfers() {
            var email = User.Identity?.Name;

            var user = _accountDetailsService.GetAccountByLogin(email);
            if (user == null)
            {
                return Unauthorized();
            }
            var transfers = _accountDetailsService.GetLastTransferList(user.Email);

            return Ok(transfers);
        }



        [HttpGet("UserAccounts")]
        [Authorize]
        public IActionResult GetUserAccounts()
        {
            var user = User.Identity?.Name;
            var userId = _accountDetailsService.GetUserId(user);
            var list = _accountDetailsService.GetUserAccountList(userId);
            return Ok(list);
        }
    }
}
