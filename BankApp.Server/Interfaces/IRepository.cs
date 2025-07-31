using BankApp.Server.DTO;
using BankApp.Server.Models;

namespace BankApp.Server.Interfaces
{
    public interface IRepository
    {
        BaseAccount GetAccountByEmail(string username);

        BaseAccount GetAccountByNumber(string number);

        User GetUserByPesel(string pesel);
        int GetUserIdByAccount(int userId);
        bool DoesUserExists(string pesel);
        bool DoesCompanyExistx(string nip);
        void IncreaceBalance(decimal amount, string accountNumber);

        void DecreaseBalance(decimal amount, string accountNumber);

        void SaveTransfer(BaseTransfer transfer);

        List<TransferDTO> GetLastAccountTransfers(string accountNumber);

        void CreateNewUser(User user);

        void CreateNewPersonalAccount(BaseAccount account);

        void CreateNewCompanyAccount(CompanyAccount companyAccount);

        List<BaseAccount> GetAccountsByUserId(int userId);

        int GetUserByAccountEmail(string username);

        BaseTransfer GetTransferById(int id);
    }
}
