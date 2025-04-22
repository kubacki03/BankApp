using BankApp.Server.DTO;

namespace BankApp.Server.Interfaces
{
    public interface IAccount
    {

          AccountDetailsDTO GetAccountDetails();
    }
}
