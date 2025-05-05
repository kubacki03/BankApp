
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

        

        public BaseAccount GetAccountByLogin(string username) => _context.Accounts.FirstOrDefault(p => p.Login == username);

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
            _context.SaveChanges();
        }

        public ICollection<BaseTransfer> GetUserTransfers(string accountNumber)
        {
           return _context.Accounts.FirstOrDefault(p=>p.Iban==accountNumber).Transfers;
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
        }

        public void CreateNewPersonalAccount(BaseAccount account)
        {
            _context.Accounts.Add(account);
        }

        public void CreateNewCompanyAccount(CompanyAccount companyAccount)
        {
           _context.CompanyAccounts.Add(companyAccount);
        }
    }
}
