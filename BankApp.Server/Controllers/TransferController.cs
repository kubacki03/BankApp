using BankApp.Server.DTO;
using BankApp.Server.Interfaces;
using BankApp.Server.Models;
using BankApp.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BankApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferController : Controller
    {
        Interfaces.IAccount service;
        ITransfer transferService;
        public TransferController(Interfaces.IAccount accountDetailsService, ITransfer transfer)
        {
            this.service = accountDetailsService;
            this.transferService = transfer;
        }

        [HttpPost("MakeTransfer")]
        [Authorize]
        public IActionResult MakeTransfer(TransferRequest request)
        {
            var email = User.Identity?.Name;
            var senderAccount = service.GetAccountByLogin(email);
            
         

            TransferModelRequest transferModelRequest = new TransferModelRequest { Amount = request.Amount, RecipientAccountNumber = request.RecipientAccountNumber, Date = DateTime.Now, Title = request.Title, SenderAccountNumber = senderAccount.Iban};
            try
            {
                transferService.SendTransfer(transferModelRequest);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            

            return Ok();
        }
     
        [HttpGet("confirmation/{transferId}")]
        public IActionResult GetTransferConfirmation(int transferId)
        {
           

           

            var pdfBytes = transferService.GenerateConfirmation(transferId);

            return File(pdfBytes, "application/pdf", $"Potwierdzenie_{transferId}.pdf");
        }

    }
}
