using BankApp.Server.DTO;
using BankApp.Server.Interfaces;
using BankApp.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TransferController : ControllerBase
    {
        private readonly IAccount _accountService;
        private readonly ITransfer _transferService;

        public TransferController(IAccount accountService, ITransfer transferService)
        {
            _accountService = accountService;
            _transferService = transferService;
        }

        [HttpPost("MakeTransfer")]
        public async Task<IActionResult> MakeTransfer(TransferRequest request, CancellationToken cancellationToken)
        {
            var email = User.Identity?.Name;
            var senderAccount = await _accountService.GetAccountByLoginAsync(email, cancellationToken);

            if (senderAccount == null)
            {
                return Unauthorized();
            }

            var transferModelRequest = new TransferModelRequest
            {
                Amount = request.Amount,
                RecipientAccountNumber = request.RecipientAccountNumber,
                Date = DateTime.Now,
                Title = request.Title,
                SenderAccountNumber = senderAccount.Iban
            };

            try
            {
                await _transferService.SendTransferAsync(transferModelRequest, cancellationToken);
            }
            catch (TransferException ex)
            {
                return Conflict(ex.Message);
            }

            return Ok();
        }

        [HttpGet("confirmation/{transferId}")]
        public async Task<IActionResult> GetTransferConfirmation(int transferId, CancellationToken cancellationToken)
        {
            var pdfBytes = await _transferService.GenerateConfirmationAsync(transferId, User.Identity?.Name, cancellationToken);

            if (pdfBytes == null)
            {
                return NotFound();
            }

            return File(pdfBytes, "application/pdf", $"Potwierdzenie_{transferId}.pdf");
        }
    }
}
