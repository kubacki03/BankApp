using AutoMapper;
using System.Security.Claims;
using BankApp.Server.DTO;
using BankApp.Server.Services;
using Microsoft.AspNetCore.Http;
using BankApp.Server.Models;

namespace BankApp.Server.Interfaces
{
    public interface IAccount
    {

        int GetUserId(string accountId);
          AccountDetailsDTO GetAccountDetails(string email);
        public bool DoesUserExistByPesel(string pesel);

        List<TransferDTO> GetLastTransferList(string login);

        public User GetUserByPesel(string pesel);
        BaseAccount GetAccountByLogin(string login);

        BaseAccount GetAccountByAccountNumber(string number);

        public List<AccountDetailsDTO> GetUserAccountList(int userId);
    }
}
