using BankApp.Server.DTO;
using BankApp.Server.Models;

namespace BankApp.Server.Interfaces
{
    public interface IRepository
    {
        Task<BaseAccount?> GetAccountByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task<BaseAccount?> FindAccountByNumberAsync(string number, CancellationToken cancellationToken = default);

        Task<User?> GetUserByPeselAsync(string pesel, CancellationToken cancellationToken = default);

        Task<int> GetUserIdByAccountAsync(int accountId, CancellationToken cancellationToken = default);

        Task<bool> DoesUserExistAsync(string pesel, CancellationToken cancellationToken = default);

        Task<bool> DoesCompanyExistAsync(string nip, CancellationToken cancellationToken = default);

        Task<bool> TryExecuteTransferAsync(BaseTransfer transfer, CancellationToken cancellationToken = default);

        Task<List<TransferDTO>> GetLastAccountTransfersAsync(string email, CancellationToken cancellationToken = default);

        Task CreateNewUserAsync(User user, CancellationToken cancellationToken = default);

        Task CreateNewPersonalAccountAsync(BaseAccount account, CancellationToken cancellationToken = default);

        Task CreateNewCompanyAccountAsync(CompanyAccount companyAccount, CancellationToken cancellationToken = default);

        Task<List<BaseAccount>> GetAccountsByUserIdAsync(int userId, CancellationToken cancellationToken = default);

        Task<int> GetUserByAccountEmailAsync(string email, CancellationToken cancellationToken = default);

        Task<BaseTransfer?> GetTransferByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
