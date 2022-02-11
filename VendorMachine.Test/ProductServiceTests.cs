using Xunit;
using Moq;
using VendorMachine.Services;
using VendorMachine.Interfaces;
using VendorMachine.Models;
using AutoFixture;

namespace VendorMachine.Services.Tests
{
 
    public class ProductServiceTests
    {
        private ProductService _sevice;       
        Mock<IProductRepository> _repositoryMock;
        private readonly IFixture _fixture = new Fixture();

        public ProductServiceTests()
        {  
            _repositoryMock = new Mock<IProductRepository>();
            _sevice = new ProductService(_repositoryMock.Object);          
        }
        
        [Fact]
        public void AddProducts_WithProducts_ReturnsProducts()
        {
            var getProducts = new GetProductResponse()
            {
                ResponseData = new System.Collections.Generic.List<ProductModel>
                {
                    new ProductModel { ProductName = "A",ProductId = 1},
                    new ProductModel { ProductName = "B",ProductId = 2},
                    new ProductModel { ProductName = "C",ProductId = 2}
                }
            };

            var products = new ProductModel { ProductName = "A",ProductId = 1 };
            _repositoryMock.Setup(x => x.Add(It.IsAny<ProductModel>())).Returns(getProducts);
            var result = _sevice.AddProducts(products);
            Assert.True(result.ResponseData.Count == getProducts.ResponseData.Count);
        }

        [Fact]
        public void AddProducts_WithNoProducts_ReturnsMessage()
        {
            var getProducts = new GetProductResponse()
            {
                ResponseData = new System.Collections.Generic.List<ProductModel>
                {
                    new ProductModel { ProductName = "A",ProductId = 1},
                    new ProductModel { ProductName = "B",ProductId = 2},
                    new ProductModel { ProductName = "C",ProductId = 2}
                }
            };

            var products = new ProductModel();
            _repositoryMock.Setup(x => x.Add(It.IsAny<ProductModel>())).Returns(getProducts);
            var result = _sevice.AddProducts(products);
            Assert.Equal( "No products to be added", result.Messages.Text);
        }

        [Fact]
        public void GetProduct_withProductName_returnsProduct()
        {
            var getProducts = new GetProductResponse()
            {
                ResponseData = new System.Collections.Generic.List<ProductModel>
                {
                    new ProductModel { ProductName = "A",ProductId = 1},
                    new ProductModel { ProductName = "B",ProductId = 2},
                    new ProductModel { ProductName = "C",ProductId = 2}
                }
            };
            string productName = "A";

            var products = new ProductModel();
            _repositoryMock.Setup(x => x.Get()).Returns(getProducts);
            var result = _sevice.GetProduct(productName);
            Assert.True(result.ProductName == productName);
        }

        [Fact]
        public void GetProductStock_WithProducts_RetunsQantity()
        {
            int expectedQuantity = 10;
            var getProducts = new GetProductResponse()
            {
                ResponseData = new System.Collections.Generic.List<ProductModel>
                {
                    new ProductModel { ProductName = "A",ProductId = 1,Quantity = 10},
                    new ProductModel { ProductName = "B",ProductId = 2,Quantity = 1},
                    new ProductModel { ProductName = "C",ProductId = 3,Quantity = 10}
                }
            };
            string productName = "A";

            var products = new ProductModel();
            _repositoryMock.Setup(x => x.Get()).Returns(getProducts);
            var result = _sevice.GetProductStock(productName);
            Assert.True(result == expectedQuantity);
        }

        [Fact]
        public void UpdateQuantity_WithProduct_returns()
        {
            var products = new ProductModel { ProductName = "A", ProductId = 1 };
            var getProducts = new GetProductResponse()
            {
                ResponseData = new System.Collections.Generic.List<ProductModel>
                {
                    new ProductModel { ProductName = "A",ProductId = 1,Quantity = 10}
                    
                }
            };
           
            
            _repositoryMock.Setup(x => x.Update(It.IsAny<ProductModel>())).Returns(getProducts);
            var result = _sevice.UpdateQuantity(products);
            Assert.True(result.ResponseData.Count == 1);
        }

        [Fact]
        public void GetAllProductTest()
        {
            var getProducts = new GetProductResponse()
            {
                ResponseData = new System.Collections.Generic.List<ProductModel>
                {
                    new ProductModel { ProductName = "A",ProductId = 1},
                    new ProductModel { ProductName = "B",ProductId = 2},
                    new ProductModel { ProductName = "C",ProductId = 2}
                }
            };
            string productName = "A";

            var products = new ProductModel();
            _repositoryMock.Setup(x => x.Get()).Returns(getProducts);
            var result = _sevice.GetAllProduct();
            Assert.True(result.ResponseData.Count == getProducts.ResponseData.Count);
        }

        [Fact]
        public void GetProductById_WithProductId_RetunsProduct()
        {
            var getProducts = new GetProductResponse()
            {
                ResponseData = new System.Collections.Generic.List<ProductModel>
                {
                    new ProductModel { ProductName = "A",ProductId = 1},
                    new ProductModel { ProductName = "B",ProductId = 2},
                    new ProductModel { ProductName = "C",ProductId = 2}
                }
            };
            string productName = "A";

            var products = new ProductModel { ProductName = "A", ProductId = 1 };
            _repositoryMock.Setup(x => x.Get()).Returns(getProducts);
            var result = _sevice.GetProductById(1);
            Assert.True(result.ProductName == productName);
        }
    }
}