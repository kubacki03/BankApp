using AutoMapper;
using BankApp.Server.DTO;
using BankApp.Server.Interfaces;
using BankApp.Server.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BankApp.Server.Services
{
    public class AccountDetailsService : IAccount
    {
        private readonly RepositoryService repositoryService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly Mapper mapper;
        public AccountDetailsService(RepositoryService repositoryService, IHttpContextAccessor httpContextAccessor, Mapper mapper)
        {
            this.repositoryService = repositoryService;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }

        public AccountDetailsDTO GetAccountDetails(string email)
        {


            var account = repositoryService.GetAccountByEmail(email);
           return new AccountDetailsDTO { AccountNumber=account.Iban , Balance=account.Balance };

           
        }


        public bool DoesUserExistByPesel(string pesel) { 
        return repositoryService.DoesUserExists(pesel);
        }

        public List<TransferDTO> GetLastTransferList(string login)
        {
            return repositoryService.GetLastAccountTransfers(login);
        }

        public User GetUserByPesel(string pesel)
        {
            return repositoryService.GetUserByPesel(pesel);
        }

        public BaseAccount GetAccountByLogin(string login)
        {
            return repositoryService.GetAccountByEmail(login);
        }

        public BaseAccount GetAccountByAccountNumber(string number)
        {
            return repositoryService.GetAccountByNumber(number);
        }
    }
}
