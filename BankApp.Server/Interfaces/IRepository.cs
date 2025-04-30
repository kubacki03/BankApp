using BankApp.Server.Models;

namespace BankApp.Server.Interfaces
{
    public interface IRepository
    {
        BaseAccount GetAccountByLogin(string username);

        BaseAccount GetAccountByNumber(string number);

        void IncreaceBalance(decimal amount, string accountNumber);

        void DecreaseBalance(decimal amount, string accountNumber);

        void SaveTransfer(BaseTransfer transfer);

        ICollection<BaseTransfer> GetUserTransfers(string accountNumber);
    }
}
