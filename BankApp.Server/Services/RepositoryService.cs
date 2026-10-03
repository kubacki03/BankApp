using BankApp.Server.DTO;
using BankApp.Server.Interfaces;
using BankApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Server.Services
{
    public class RepositoryService : IRepository
    {
        private readonly AppDbContext _context;

        public RepositoryService(AppDbContext context)
        {
            _context = context;
        }

        public Task<BaseAccount?> GetAccountByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return _context.Accounts.AsNoTracking().FirstOrDefaultAsync(p => p.Email == email, cancellationToken);
        }

        public Task<BaseAccount?> FindAccountByNumberAsync(string number, CancellationToken cancellationToken = default)
        {
            return _context.Accounts.AsNoTracking().FirstOrDefaultAsync(p => p.Iban == number, cancellationToken);
        }

        public async Task<bool> TryExecuteTransferAsync(BaseTransfer transfer, CancellationToken cancellationToken = default)
        {
            var amount = transfer.Amount;
            var senderId = transfer.SenderId;
            var payeeId = transfer.PayeeId;

            await using var dbTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            var debited = await _context.Accounts
                .Where(a => a.Id == senderId && a.IsActive && a.Balance >= amount)
                .ExecuteUpdateAsync(s => s.SetProperty(a => a.Balance, a => a.Balance - amount), cancellationToken);

            if (debited != 1)
            {
                return false;
            }

            var credited = await _context.Accounts
                .Where(a => a.Id == payeeId && a.IsActive)
                .ExecuteUpdateAsync(s => s.SetProperty(a => a.Balance, a => a.Balance + amount), cancellationToken);

            if (credited != 1)
            {
                return false;
            }

            _context.Transfers.Add(transfer);
            await _context.SaveChangesAsync(cancellationToken);
            await dbTransaction.CommitAsync(cancellationToken);
            return true;
        }

        public Task<bool> DoesUserExistAsync(string pesel, CancellationToken cancellationToken = default)
        {
            return _context.Users.AnyAsync(p => p.Pesel == pesel, cancellationToken);
        }

        public Task<bool> DoesCompanyExistAsync(string nip, CancellationToken cancellationToken = default)
        {
            return _context.CompanyAccounts.AnyAsync(p => p.NIP == nip, cancellationToken);
        }

        public async Task CreateNewUserAsync(User user, CancellationToken cancellationToken = default)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CreateNewPersonalAccountAsync(BaseAccount account, CancellationToken cancellationToken = default)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync(cancellationToken);
            account.User.DefaulAccount = account;
            account.User.DefaultAccountId = account.Id;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CreateNewCompanyAccountAsync(CompanyAccount companyAccount, CancellationToken cancellationToken = default)
        {
            _context.CompanyAccounts.Add(companyAccount);
            await _context.SaveChangesAsync(cancellationToken);

            companyAccount.User.DefaulAccount = companyAccount;
            companyAccount.User.DefaultAccountId = companyAccount.Id;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<TransferDTO>> GetLastAccountTransfersAsync(string email, CancellationToken cancellationToken = default)
        {
            var transfers = await _context.Transfers
                .AsNoTracking()
                .Where(t => t.Sender.Email == email)
                .OrderByDescending(t => t.Date)
                .Take(5)
                .Select(t => new { t.Id, t.Amount, t.Date, t.Title, PayeeName = t.Payee.User.Name })
                .ToListAsync(cancellationToken);

            return transfers
                .Select(t => new TransferDTO { Id = t.Id, Amount = t.Amount, Date = t.Date.ToShortDateString(), PayeeName = t.PayeeName, Title = t.Title })
                .ToList();
        }

        public Task<User?> GetUserByPeselAsync(string pesel, CancellationToken cancellationToken = default)
        {
            return _context.Users.AsNoTracking().FirstOrDefaultAsync(p => p.Pesel == pesel, cancellationToken);
        }

        public Task<List<BaseAccount>> GetAccountsByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return _context.Accounts.AsNoTracking().Where(a => a.UserId == userId).ToListAsync(cancellationToken);
        }

        public Task<int> GetUserIdByAccountAsync(int accountId, CancellationToken cancellationToken = default)
        {
            return _context.Accounts.Where(a => a.Id == accountId).Select(a => a.UserId).FirstOrDefaultAsync(cancellationToken);
        }

        public Task<int> GetUserByAccountEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return _context.Accounts.Where(a => a.Email == email).Select(a => a.UserId).FirstOrDefaultAsync(cancellationToken);
        }

        public Task<BaseTransfer?> GetTransferByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return _context.Transfers
                .AsNoTracking()
                .Include(a => a.Payee).ThenInclude(a => a.User)
                .Include(a => a.Sender).ThenInclude(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }
    }
}
