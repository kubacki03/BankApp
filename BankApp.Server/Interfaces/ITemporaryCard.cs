using BankApp.Server.Models;

namespace BankApp.Server.Interfaces
{
    public interface ITemporaryCard
    {
        DebitCard GenerateTemporaryDebitCard(string email);
    }
}
