using BankApp.Server.Interfaces;
using BankApp.Server.Models;
using BankApp.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CardController : Controller
    {
        private readonly ITemporaryCard _service;

        public CardController(ITemporaryCard temporaryCardService)
        {
            _service = temporaryCardService;
        }

        [HttpGet("GetCard")]
        public IActionResult ShowTemporaryCard()
        {
            var userName = User.Identity?.Name;
            var card = HttpContext.Session.GetObject<DebitCard>(userName);

            if(card!=null && card.Lifetime < DateTime.Now)
            {
                HttpContext.Session.Remove(userName);
                card=null;
            }

            if (card == null)
            { 
               card = _service.GenerateTemporaryDebitCard(userName);
               HttpContext.Session.SetObject(userName, card);
            }

            return Ok(card);
        }
    }
}
