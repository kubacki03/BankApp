using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using AutoMapper;
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
        private readonly Mapper mapper;

        public AuthService(IRepository repository, IConfiguration config, Mapper mapper)
        {
            repositoryService = repository;
            _config = config;
            mapper = mapper;
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

    
           var account= repositoryService.GetAccountByEmail(modelRequest.email);
            if (account == null)
            {
                return null;
            }
            var passwordHasher = new PasswordHasher<BaseAccount>();

            var result = passwordHasher.VerifyHashedPassword(account, account.Password, modelRequest.password);

            
            if (result == PasswordVerificationResult.Success)
            {
                var claims = new[]
                  {
                    new Claim(ClaimTypes.Name, account.Email),
                  
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Issuer"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);


                return new JwtSecurityTokenHandler().WriteToken(token);
                    

            }
            return null;
        }

        public bool Register(RegisterModelRequest modelRequest)
        {
            if (repositoryService.DoesUserExists(modelRequest.pesel))
            {
                return false;
            }

            if (modelRequest.nip!=null && repositoryService.DoesCompanyExistx(modelRequest.nip))
            {
                return false;
            }

            var newUser= mapper.Map<User>(modelRequest);

            BaseAccount account = null;
            var passwordHasher = new PasswordHasher<BaseAccount>();
            var hashedPassword = passwordHasher.HashPassword(account, modelRequest.password);
            repositoryService.CreateNewUser(newUser);
            

            var curentTime = DateTime.Now.Millisecond;
            var hashCode = modelRequest.GetHashCode();
            BigInteger number = BigInteger.Abs(curentTime * hashCode);

            
            string numberStr = number.ToString();

            
            if (numberStr.Length < 26)
                numberStr = numberStr.PadLeft(26, '0');

            
            if (numberStr.Length > 26)
                numberStr = numberStr.Substring(0, 26);


            if (modelRequest.companyName != null)
            {
                account = new CompanyAccount { Balance = 0, Email = modelRequest.email, IsActive = true, Password = hashedPassword, Iban=numberStr};
                var login = account.GetHashCode();
                account.Login = BigInteger.Abs(login).ToString();
            }
            else
            {
                account = new BaseAccount { Balance = 0, Email= modelRequest.email,IsActive = true, Password = hashedPassword, Iban=numberStr };
                var login = account.GetHashCode();
                account.Login=BigInteger.Abs(login).ToString();
            }

            account.UserId = newUser.Id;
            if (account is CompanyAccount)
            {
                repositoryService.CreateNewCompanyAccount((CompanyAccount)account);
            }
            else if (account is BaseAccount)
            {
                repositoryService.CreateNewPersonalAccount(account);
            }




            return true;
        }
    }
}
