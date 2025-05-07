using AutoMapper;
using System.Security.Claims;
using BankApp.Server.DTO;
using BankApp.Server.Services;
using Microsoft.AspNetCore.Http;

namespace BankApp.Server.Interfaces
{
    public interface IAccount
    {

          AccountDetailsDTO GetAccountDetails();
        public bool DoesUserExistByPesel(string pesel);

        List<TransferDTO> GetLastTransferList(string iban);
    }
}
