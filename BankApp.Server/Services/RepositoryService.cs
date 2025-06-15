
using BankApp.Server.DTO;
using BankApp.Server.Interfaces;
using BankApp.Server.Models;
using Microsoft.AspNetCore.Identity;
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



        public BaseAccount GetAccountByEmail(string email)
        {
            return _context.Accounts.Include(a=>a.Transfers).FirstOrDefault(p => p.Email == email);
        }

        public BaseAccount GetAccountByNumber(string number)
        {

            var account = _context.Accounts.FirstOrDefault(p => p.Iban == number);

            if (account == null)
            {
                throw new InvalidOperationException($"Nie znaleziono konta o numerze: {number}");
            }

            return account;


        }

        public void IncreaceBalance(decimal amount, string accountNumber)
        {
            _context.Accounts.FirstOrDefault(p => p.Iban == accountNumber).Balance += amount;
            _context.SaveChanges();
        }

        public void DecreaseBalance(decimal amount, string accountNumber)
        {
            _context.Accounts.FirstOrDefault(p => p.Iban == accountNumber).Balance -= amount;
            _context.SaveChanges();
        }

        public void SaveTransfer(BaseTransfer transfer)
        {
            _context.Transfers.Add(transfer);
            transfer.Payee.Transfers.Add(transfer);
            transfer.Sender.Transfers.Add(transfer);
            _context.SaveChanges();
        }

        public bool DoesUserExists(string pesel)
        {
            return _context.Users.Any(p=>p.Pesel==pesel);
          
        }

        public bool DoesCompanyExistx(string nip)
        {
           return _context.CompanyAccounts.Any(p=>p.NIP==nip);
        }

        public void CreateNewUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void CreateNewPersonalAccount(BaseAccount account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
            account.User.DefaulAccount = account;
            account.User.DefaultAccountId = account.Id;
            _context.SaveChanges();
        }

        public void CreateNewCompanyAccount(CompanyAccount companyAccount)
        {
           _context.CompanyAccounts.Add(companyAccount);
            
            _context.SaveChanges();

            companyAccount.User.DefaulAccount = companyAccount;
            companyAccount.User.DefaultAccountId = companyAccount.Id;
            _context.SaveChanges();
        }

        public List<TransferDTO> GetLastAccountTransfers(string email)
        {

            var account = _context.Accounts
     .Include(a => a.Transfers)
         .ThenInclude(t => t.Payee)
             .ThenInclude(p => p.User)
     .FirstOrDefault(p => p.Email == email);

            return account?.Transfers
                .Select(x => new TransferDTO { Amount = x.Amount, Date = x.Date.ToShortDateString(), PayeeName = x.Payee.User.Name, Title = x.Title })
                .Take(5)
                .ToList() ?? new List<TransferDTO>();

        }

        public User GetUserByPesel(string pesel)
        {
            return _context.Users.FirstOrDefault(p => p.Pesel == pesel);
        }
    }
}
