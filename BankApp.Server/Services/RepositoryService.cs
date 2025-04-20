
using BankApp.Server.Interfaces;
using BankApp.Server.Models;
using Microsoft.AspNetCore.Identity;

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
            
 
        
    }
}
