using AutoFixture;
using VendorMachine.Models;
using Xunit;

namespace VendorMachine.Repositories.Tests
{
    public class ProductRepositoryTests
    {
        private ProductRepository _productRepository;
        private readonly IFixture _fixture = new Fixture();
        public ProductRepositoryTests()
        {
            _productRepository = new ProductRepository();           
        }

        [Fact]
        public void Add_WithProduct_Returns()
        {          

            var products = new ProductModel { ProductName = "A",ProductId = 1 };
        
            var result = _productRepository.Add(products);
            Assert.True(result.ResponseData.Count > 0);
        }

        [Fact]
        public void Update_GivenProduct_returnsUpdatedProduct()
        {
            var products = new ProductModel { ProductName = "A", ProductId = 1 };

            var result = _productRepository.Update(products);
            Assert.True(result.ResponseData.Count > 0);
        }

        [Fact]
        public void Get_ReturnsProducts()
        {
            var result = _productRepository.Get();
            Assert.True(result.ResponseData.Count > 0);
        }      
        public void hasItem_ReturnsTrue()
        {
            string item = "A";
            var result = _productRepository.hasItem(item);
            Assert.True(result);
        }
    }
}