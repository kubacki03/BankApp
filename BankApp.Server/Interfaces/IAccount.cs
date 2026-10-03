using BankApp.Server.DTO;
using BankApp.Server.Models;

namespace BankApp.Server.Interfaces
{
    public interface IAccount
    {
        Task<int> GetUserIdAsync(string email, CancellationToken cancellationToken = default);

        Task<AccountDetailsDTO?> GetAccountDetailsAsync(string email, CancellationToken cancellationToken = default);

        Task<bool> DoesUserExistByPeselAsync(string pesel, CancellationToken cancellationToken = default);

        Task<List<TransferDTO>> GetLastTransferListAsync(string email, CancellationToken cancellationToken = default);

        Task<User?> GetUserByPeselAsync(string pesel, CancellationToken cancellationToken = default);

        Task<BaseAccount?> GetAccountByLoginAsync(string login, CancellationToken cancellationToken = default);

        Task<List<AccountDetailsDTO>> GetUserAccountListAsync(int userId, CancellationToken cancellationToken = default);
    }
}
