
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

        

        public BaseAccount GetAccountByLogin(string username) => _context.accounts.FirstOrDefault(p => p.Login == username);

        public BaseAccount GetAccountByNumber(string number)
        {

            var account = _context.accounts.FirstOrDefault(p => p.AccountNumber == number);

            if (account == null)
            {
                throw new InvalidOperationException($"Nie znaleziono konta o numerze: {number}");
            }

            return account;


        }

        public void IncreaceBalance(decimal amount, string accountNumber)
        {
            _context.accounts.FirstOrDefault(p => p.AccountNumber == accountNumber).Balance += amount;
            _context.SaveChanges();
        }

        public void DecreaseBalance(decimal amount, string accountNumber)
        {
            _context.accounts.FirstOrDefault(p => p.AccountNumber == accountNumber).Balance -= amount;
            _context.SaveChanges();
        }

        public void SaveTransfer(BaseTransfer transfer)
        {
            _context.transfers.Add(transfer);
            _context.SaveChanges();
        }

        public ICollection<BaseTransfer> GetUserTransfers(string accountNumber)
        {
           return _context.accounts.FirstOrDefault(p=>p.AccountNumber==accountNumber).Transfers;
        }
    }
}
