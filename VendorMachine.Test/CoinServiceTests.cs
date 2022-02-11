using Xunit;
using Moq;
using VendorMachine.Services;
using VendorMachine.Interfaces;
using VendorMachine.Models;
using AutoFixture;
using System;

namespace VendorMachine.Services.Tests
{

    public class CoinServiceTests
    {
        private CoinService _coinSevice;
        Mock<ICoinService> _mockCoinSevice;
        private readonly IFixture _fixture = new Fixture();

        public CoinServiceTests()
        {
            _coinSevice = new CoinService();
          _mockCoinSevice = new Mock<ICoinService>();
        }

        [Fact]
        public void IsCoinValid_WithValidCoin_ReturnsTrue()
        {
            // Arrange
            decimal amount = 0.10M;

            // Act
            var result = _coinSevice.IsCoinValid(amount);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsCoinValid_WithInValidCoin_ReturnsFalse()
        {
            // Arrange
            decimal amount = 0.010M;

            // Act
            var result = _coinSevice.IsCoinValid(amount);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AcceptCoins_WithValidCoin_ReturnsTotalAmount()
        {
            // Arrange
            decimal amount = 0.100M;
            var ExpectedResult = new GetCoinResponse()
            {
                ResponseData = new CoinModel
                {
                    Value = .100M
                },
                Success = true
            };

            //Act
            var result = _coinSevice.AcceptCoins(amount);

            // Assert
            Assert.Equal(ExpectedResult.ResponseData.Value, result.ResponseData.Value);
        }

        [Fact]
        public void AcceptCoins_WithInValidCoin_ReturnsMessage()
        {
            // Arrange
            decimal amount = 0.01M;
            var ExpectedResult = new GetCoinResponse()
            {
                ResponseData = new CoinModel
                {
                    Value = .100M
                },
                Success = true
            };

            //Act
            var result = _coinSevice.AcceptCoins(amount);

            // Assert
            Assert.Equal("Insert valid Coin", result.Messages.Text);
        }

        [Fact]
        public void AcceptCoins_WithCoinValueAsZero_ReturnsMessage()
        {
            // Arrange
            decimal amount = 0.0m;
            var ExpectedResult = new GetCoinResponse()
            {
                ResponseData = new CoinModel
                {
                    Value = .100M
                },
                Success = true
            };

            //Act
            var result = _coinSevice.AcceptCoins(amount);

            // Assert
            Assert.Equal("Input Coin", result.Messages.Text);
        }

    }
}