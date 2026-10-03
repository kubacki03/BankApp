using BankApp.Server.DTO;
using BankApp.Server.Interfaces;
using BankApp.Server.Models;

namespace BankApp.Server.Services
{
    public class AccountDetailsService : IAccount
    {
        private readonly IRepository _repository;

        public AccountDetailsService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<AccountDetailsDTO?> GetAccountDetailsAsync(string email, CancellationToken cancellationToken = default)
        {
            var account = await _repository.GetAccountByEmailAsync(email, cancellationToken);

            if (account == null)
            {
                return null;
            }

            return new AccountDetailsDTO { AccountNumber = account.Iban, Balance = account.Balance };
        }

        public Task<bool> DoesUserExistByPeselAsync(string pesel, CancellationToken cancellationToken = default)
        {
            return _repository.DoesUserExistAsync(pesel, cancellationToken);
        }

        public Task<List<TransferDTO>> GetLastTransferListAsync(string email, CancellationToken cancellationToken = default)
        {
            return _repository.GetLastAccountTransfersAsync(email, cancellationToken);
        }

        public Task<User?> GetUserByPeselAsync(string pesel, CancellationToken cancellationToken = default)
        {
            return _repository.GetUserByPeselAsync(pesel, cancellationToken);
        }

        public Task<BaseAccount?> GetAccountByLoginAsync(string login, CancellationToken cancellationToken = default)
        {
            return _repository.GetAccountByEmailAsync(login, cancellationToken);
        }

        public async Task<List<AccountDetailsDTO>> GetUserAccountListAsync(int userId, CancellationToken cancellationToken = default)
        {
            var accounts = await _repository.GetAccountsByUserIdAsync(userId, cancellationToken);

            return accounts
                .Select(a => new AccountDetailsDTO { AccountNumber = a.Iban, Balance = a.Balance, Name = a.Name })
                .ToList();
        }

        public Task<int> GetUserIdAsync(string email, CancellationToken cancellationToken = default)
        {
            return _repository.GetUserByAccountEmailAsync(email, cancellationToken);
        }
    }
}
