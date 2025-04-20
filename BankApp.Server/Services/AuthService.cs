using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BankApp.Server.DTO;
using BankApp.Server.Interfaces;
using BankApp.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BankApp.Server.Services
{
    public class AuthService : ILogin, IRegister
    {

        private readonly IRepository repositoryService;
        private readonly IConfiguration _config;
        public AuthService(IRepository repository, IConfiguration config)
        {
            this.repositoryService = repository;
            this._config = config;
        }
        public bool DoesUserExist(RegisterModelRequest modelRequest)
        {
            throw new NotImplementedException();
        }

        public string GenerateTempPassword(string username)
        {
            throw new NotImplementedException();
        }

        public  string Login(LoginModelRequest modelRequest)
        {

           //getting account from database
           var account= repositoryService.GetAccountByLogin(modelRequest.Login);
            if (account == null)
            {
                return null;
            }
            var passwordHasher = new PasswordHasher<BaseAccount>();

            var result = passwordHasher.VerifyHashedPassword(account, account.Password, modelRequest.Password);

            
            if (result == PasswordVerificationResult.Success)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, modelRequest.Login),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Issuer"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: creds);


                return new JwtSecurityTokenHandler().WriteToken(token);
                    

            }
            return null;
        }

        public bool Register(RegisterModelRequest modelRequest)
        {
            throw new NotImplementedException();
        }
    }
}
