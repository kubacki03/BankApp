using BankApp.Server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AccountDetailsController : ControllerBase
    {
        private readonly IAccount _accountDetailsService;

        public AccountDetailsController(IAccount accountDetailsService)
        {
            _accountDetailsService = accountDetailsService;
        }

        [HttpGet("account")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var email = User.Identity?.Name;
            var detailsDTO = await _accountDetailsService.GetAccountDetailsAsync(email, cancellationToken);

            if (detailsDTO == null)
            {
                return NotFound();
            }

            return Ok(detailsDTO);
        }

        [HttpGet("lastTransfers")]
        public async Task<IActionResult> GetLastTransfers(CancellationToken cancellationToken)
        {
            var email = User.Identity?.Name;

            var account = await _accountDetailsService.GetAccountByLoginAsync(email, cancellationToken);
            if (account == null)
            {
                return Unauthorized();
            }

            var transfers = await _accountDetailsService.GetLastTransferListAsync(account.Email, cancellationToken);

            return Ok(transfers);
        }

        [HttpGet("UserAccounts")]
        public async Task<IActionResult> GetUserAccounts(CancellationToken cancellationToken)
        {
            var email = User.Identity?.Name;
            var userId = await _accountDetailsService.GetUserIdAsync(email, cancellationToken);
            var list = await _accountDetailsService.GetUserAccountListAsync(userId, cancellationToken);

            return Ok(list);
        }
    }
}
