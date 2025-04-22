using System;
using BankApp.Server.Controllers;
using BankApp.Server.DTO;
using BankApp.Server.Interfaces;
using BankApp.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework.Legacy;

namespace  TestyBanku
{

    [TestFixture]
    public class AccountDetailsControllerTest
    {


        private Mock<IAccount> _mockService;
        private AccountDetailsController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IAccount>();
            _controller = new AccountDetailsController(_mockService.Object);
        }



        [TearDown]
        public void Cleanup()
        {
            _controller?.Dispose();
        }

        [Test]
        public void Get_ReturnsOkResult_WithAccountDetails()
        {
            // Arrange
            var expectedDetails = new AccountDetailsDTO
            {
                AccountNumber = "32432434342342324",
                Transfers = null,
                Balance = 1000
            };

            _mockService.Setup(service => service.GetAccountDetails())
                        .Returns(expectedDetails);

            var result = _controller.Get();

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            ClassicAssert.NotNull(okResult);
            ClassicAssert.AreEqual(expectedDetails, okResult.Value);
        }

    }
}