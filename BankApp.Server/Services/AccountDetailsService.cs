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

        public AccountDetailsDTO GetAccountDetails()
        {
            var user = httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                return null; 
            }

            string login = user.FindFirst(ClaimTypes.Name)?.Value;
           
             

            return mapper.Map<AccountDetailsDTO>(repositoryService.GetAccountByLogin(login));
        }
    }
}
