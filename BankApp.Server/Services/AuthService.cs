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
        private readonly IRepository _repository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<BaseAccount> _passwordHasher;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            IRepository repository,
            IConfiguration config,
            IMapper mapper,
            IPasswordHasher<BaseAccount> passwordHasher,
            ILogger<AuthService> logger)
        {
            _repository = repository;
            _config = config;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public async Task<string?> LoginAsync(LoginModelRequest modelRequest, CancellationToken cancellationToken = default)
        {
            var account = await _repository.GetAccountByEmailAsync(modelRequest.email, cancellationToken);

            if (account == null)
            {
                _logger.LogWarning("Login failed: account not found");
                return null;
            }

            var result = _passwordHasher.VerifyHashedPassword(account, account.Password, modelRequest.password);

            if (result == PasswordVerificationResult.Failed)
            {
                _logger.LogWarning("Login failed: invalid password for account {AccountId}", account.Id);
                return null;
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, account.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            _logger.LogInformation("Account {AccountId} logged in", account.Id);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> RegisterAsync(RegisterModelRequest modelRequest, CancellationToken cancellationToken = default)
        {
            if (await _repository.DoesUserExistAsync(modelRequest.pesel, cancellationToken))
            {
                return false;
            }

            if (modelRequest.nip != null && await _repository.DoesCompanyExistAsync(modelRequest.nip, cancellationToken))
            {
                return false;
            }

            var newUser = _mapper.Map<User>(modelRequest);

            var hashedPassword = _passwordHasher.HashPassword(null!, modelRequest.password);
            await _repository.CreateNewUserAsync(newUser, cancellationToken);

            var curentTime = DateTime.Now.Millisecond;
            var hashCode = modelRequest.GetHashCode();
            BigInteger number = BigInteger.Abs(curentTime * hashCode);

            string numberStr = number.ToString();

            if (numberStr.Length < 26)
            {
                numberStr = numberStr.PadLeft(26, '0');
            }

            if (numberStr.Length > 26)
            {
                numberStr = numberStr.Substring(0, 26);
            }

            if (modelRequest.companyName != null)
            {
                var companyAccount = new CompanyAccount { Balance = 0, Email = modelRequest.email, IsActive = true, Password = hashedPassword, Iban = numberStr };
                companyAccount.Login = BigInteger.Abs(companyAccount.GetHashCode()).ToString();
                companyAccount.UserId = newUser.Id;
                await _repository.CreateNewCompanyAccountAsync(companyAccount, cancellationToken);
            }
            else
            {
                var account = new BaseAccount { Balance = 0, Email = modelRequest.email, IsActive = true, Password = hashedPassword, Iban = numberStr };
                account.Login = BigInteger.Abs(account.GetHashCode()).ToString();
                account.UserId = newUser.Id;
                await _repository.CreateNewPersonalAccountAsync(account, cancellationToken);
            }

            _logger.LogInformation("Registered new user {UserId}", newUser.Id);

            return true;
        }
    }
}
