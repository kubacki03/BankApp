using BankApp.Server.Interfaces;
using BankApp.Server.Models;

namespace BankApp.Server.Services
{
    public class TemporaryDebitCardService : ITemporaryCard
    {
        private static Random _random = new Random();
        public DebitCard GenerateTemporaryDebitCard(string email)
        {
            return new DebitCard
            {
                CardNumber = string.Concat(Enumerable.Range(0, 16).Select(_ => _random.Next(0, 10))),
                ExpirationDate = DateTime.Now.AddYears(2).ToString("MM/yy"),
                CVV = string.Concat(Enumerable.Range(0, 3).Select(_ => _random.Next(0, 10))),
                Lifetime = DateTime.Now.AddMinutes(30)
            };
        }
       

    }
}
