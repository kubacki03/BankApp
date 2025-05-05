using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BankApp.Server.DTO;
using BankApp.Server.Interfaces;
using BankApp.Server.Models;
using BankApp.Server.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace TestyBanku
{
    [TestFixture]
    public class TokenGeneratingTest
    {
        private Mock<IRepository> _mockRepo;

        private Mock<IConfiguration> _mockConfig;
        private AuthService _authService;
        private Mapper _mapper;
        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IRepository>();
            _mockConfig = new Mock<IConfiguration>();

            // Konfiguracja "Jwt:Key" i "Jwt:Issuer"
            _mockConfig.Setup(config => config["Jwt:Key"]).Returns("superSecretKey12345678dsaaaaaaaaadsaczxxzbcxv90"); 
            _mockConfig.Setup(config => config["Jwt:Issuer"]).Returns("TestIssuer");
            
            _authService = new AuthService(_mockRepo.Object, _mockConfig.Object, _mapper );
        }

        [Test]
        public void ServiceShouldGenerateToken_WhenCredentialsAreCorrect()
        {
            // Arrange
            var login = "testUser";
            var password = "testPassword";
            var hashedPassword = new PasswordHasher<BaseAccount>().HashPassword(null, password);

            var fakeAccount = new BaseAccount
            {
                Login = login,
                Password = hashedPassword
            };

            _mockRepo.Setup(repo => repo.GetAccountByLogin(login)).Returns(fakeAccount);

            var request = new LoginModelRequest
            {
                Login = login,
                Password = password
            };

            // Act
            var token = _authService.Login(request);

            // Assert
            ClassicAssert.NotNull(token);
            ClassicAssert.IsNotEmpty(token);
            TestContext.Out.WriteLine($"Generated Token: {token}");
        }


        [Test]
        public void ServiceShouldNoTGenerateToken_WhenCredentialsAreInCorrect()
        {
            // Arrange
            var login = "testUser";
            var badLogin = "badLogin";
            var password = "testPassword";
            var hashedPassword = new PasswordHasher<BaseAccount>().HashPassword(null, password);

            var fakeAccount = new BaseAccount
            {
                Login = login,
                Password = hashedPassword
            };

            _mockRepo.Setup(repo => repo.GetAccountByLogin(login)).Returns(fakeAccount);

            var request = new LoginModelRequest
            {
                Login = badLogin,
                Password = password
            };

            // Act
            var token = _authService.Login(request);

            // Assert
            ClassicAssert.IsNull(token);
           
          
        }
    }
}
