using Xunit;
using Moq;
using VendorMachine.Services;
using VendorMachine.Interfaces;
using VendorMachine.Models;
using AutoFixture;

namespace VendorMachine.Test
{
    public class VendingMachineServiceTest
    {
        private VendingMachineService _vendingService;
        private Mock<ICoinService> _coinSevice;
        private Mock<IProductService> _productService;       
        private readonly IFixture _fixture = new Fixture();

        public VendingMachineServiceTest()
        {
            _productService = new Mock<IProductService>();
            _coinSevice = new Mock<ICoinService>();
            _vendingService = new VendingMachineService(_coinSevice.Object, _productService.Object);

        }   

        [Fact]
        public void AcceptCoin_WithValidCoin_GivesCorrectAmount()
        {
            // Arrange
            decimal amount = 0.100M;
            string expected = $"Amount entered: {amount} , Total Amount = {amount}";
            var Messages = new MessageModel { Text = expected };

            //Act
            var coinResponse = _fixture.Build<GetCoinResponse>().
                With(c => c.Success, true)
                .With(a => a.Messages, Messages)
                .Create();
            _coinSevice.Setup(x => x.AcceptCoins(amount)).Returns(coinResponse);

            // Act
            var result = _vendingService.AcceptCoin(amount);

            // Assert
            Assert.Equal(expected, result.Messages.Text);
        }

        [Fact]
        public void AcceptCoin_WithInValidCoin_Gives()
        {
            // Arrange
            decimal amount = 0.01M;
            string expected = $"Insert valid Coin";
            var Messages = new MessageModel { Text = expected };
            //Act

            var coinResponse = _fixture.Build<GetCoinResponse>().
                With(c => c.Success, true)
                .With(a => a.Messages, Messages)
                .Create();
            _coinSevice.Setup(x => x.AcceptCoins(amount)).Returns(coinResponse);

            // Act
            var result = _vendingService.AcceptCoin(amount);

            // Assert
            Assert.Equal(expected, result.Messages.Text);
        }

        [Fact]
        public void AcceptCoin_WithoutCoin_Gives()
        {
            // Arrange
            decimal amount = 0.01M;
            string expected = $"Insert Coin";
            var Messages = new MessageModel { Text = expected };
            //Act

            var coinResponse = _fixture.Build<GetCoinResponse>().
                With(c => c.Success, true)
                .With(a => a.Messages, Messages)
                .Create();
            _coinSevice.Setup(x => x.AcceptCoins(amount)).Returns(coinResponse);

            // Act
            var result = _vendingService.AcceptCoin(amount);

            // Assert
            Assert.Equal(expected, result.Messages.Text);
        }

        [Fact]
        public void ShowProducts_WithProducts_GivesProducts()
        {
            // Arrange

            var getProducts = new GetProductResponse()
            {
                ResponseData = new System.Collections.Generic.List<ProductModel>
                {
                    new ProductModel { ProductName = "A"},
                    new ProductModel { ProductName = "B"},
                    new ProductModel { ProductName = "C"}
                }
            };
            //Act


            _productService.Setup(x => x.GetAllProduct()).Returns(getProducts);

            // Act
            var result = _vendingService.ShowProducts();

            // Assert

            Assert.True(result.ResponseData.Count == 3);
        }

        [Fact]
        public void SelectProduct_WithProductIdAsZero_GivesMessageSelectProduct()
        {
            // Arrange
            int productId = 0;

            // Act
            var result = _vendingService.SelectProduct(productId, 0);

            // Assert
            Assert.True(result.Messages.Text == "Select Product");
        }

        [Fact]
        public void SelectProduct_WithProductIdAndTotalAmountIsZero_GivesMessage()
        {
            // Arrange
            int productId = 4;
            _productService.Setup(x => x.GetProductById(productId)).Returns(It.IsAny<ProductModel>);

            // Act
            var result = _vendingService.SelectProduct(productId, 0);

            // Assert
            Assert.True(result.Messages.Text == "Insert Coin");
        }

        [Fact]
        public void SelectProduct_WithInvalidProductId_GivesMessage()
        {
            // Arrange
            int productId = 4;
            _productService.Setup(x => x.GetProductById(productId)).Returns(It.IsAny<ProductModel>);

            // Act
            var result = _vendingService.SelectProduct(productId, 0.100M);

            // Assert
            Assert.True(result.Messages.Text == "Item not in list,Try again");
        }

        [Fact]
        public void SelectProduct_WithValidProductIdAndNoStock_GivesMessage()
        {
            // Arrange
            int productId = 1;
            decimal amountInMachine = 0.100M;
            var products =
                    new ProductModel { ProductName = "A" };
            _productService.Setup(x => x.GetProductById(productId)).Returns(products);
          
            _productService.Setup(x => x.GetProductStock(It.IsAny<string>())).Returns(0);
            // Act
            var result = _vendingService.SelectProduct(productId, amountInMachine);

            // Assert
            Assert.Equal("Sold Out", result.Messages.Text);
        }

        [Fact]
        public void SelectProduct_WithValidProductId_GivesMessage()
        {
            // Arrange
            int productId = 1;
            decimal amountInMachine = 0.100M;
            var products =
                    new ProductModel { ProductName = "A" };
            _productService.Setup(x => x.GetProductById(productId)).Returns(products);

            var productStock =
                    new ProductModel { ProductName = "A" };
            _productService.Setup(x => x.GetProductStock(It.IsAny<string>())).Returns(10);
            // Act
            var result = _vendingService.SelectProduct(productId, amountInMachine);

            // Assert
            Assert.Contains("Please take your change: ",result.Messages.Text);
        }

    }
}