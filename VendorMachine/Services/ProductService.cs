using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorMachine.Enum;
using VendorMachine.Interfaces;
using VendorMachine.Models;

namespace VendorMachine.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public GetProductResponse AddProducts(ProductModel product)
        {
            if (product.ProductId == 0 || product.ProductName == null)
            {
                return new GetProductResponse()
                {
                    Messages = new MessageModel { MessageType = MessageType.Error, Text = $"No products to be added" },
                    Success = false
                };
            }

            return _productRepository.Add(product);
        }

        public ProductModel GetProduct(string productName)
        {
            return _productRepository.Get().ResponseData.FirstOrDefault(x => x.ProductName == productName);
        }

        public int GetProductStock(string productName)
        {
            return GetProduct(productName).Quantity;
        }

        public GetProductResponse UpdateQuantity(ProductModel product)
        {
            return _productRepository.Update(product);
        }

        public GetProductResponse GetAllProduct()
        {
            return _productRepository.Get();
        }

        public ProductModel GetProductById(int productId)
        {
            return _productRepository.Get().ResponseData.FirstOrDefault(x => x.ProductId == productId);
        }
    }
}
