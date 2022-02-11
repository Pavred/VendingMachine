using System;
using System.Collections.Generic;
using System.Linq;
using VendorMachine.Enum;
using VendorMachine.Interfaces;
using VendorMachine.Models;

namespace VendorMachine.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private int _nextId = 1;

        private List<ProductModel> _products = new List<ProductModel>();
        public ProductRepository()
        {
            _products = new List<ProductModel>()
            {
                new ProductModel()
                {
                    ProductId = _nextId++,
                    Price = 1.00M,
                    ProductName = "Cola",
                    Quantity = 10
                },
                new ProductModel()
                {
                    ProductId = _nextId++,
                    Price = 0.50M,
                    ProductName = "chips",
                    Quantity = 10
                },
                new ProductModel()
                {
                    ProductId = _nextId++,
                    Price = .65M,
                    ProductName = "candy",
                    Quantity = 10
                }
            };
        }


        public GetProductResponse Add(ProductModel product)
        {
            try
            {
                var response = new GetProductResponse();
                if (hasItem(product.ProductName))
                {
                    var quantity = _products.Find(x => x.ProductId == product.ProductId).Quantity;
                    product.Quantity = product.Quantity + quantity;
                    response = Update(product);
                }

                product.ProductId = _nextId++;
                _products.Add(product);

                response.ResponseData = _products;
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                return new GetProductResponse()
                {
                    Messages = new MessageModel
                    { MessageType = MessageType.Error, Text = $"Error in Add method {ex}" },
                    Success = false
                };
            }
        }

        public GetProductResponse Update(ProductModel product)
        {

            try
            {
                var response = new GetProductResponse();
                if (product == null)
                {
                    throw new ArgumentNullException("product not found");
                }
                var indexOf = _products.IndexOf(_products.Find(p => p.ProductId == product.ProductId));
                _products[indexOf] = product;

                response.ResponseData = _products;
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                return new GetProductResponse()
                {
                    Messages = new MessageModel { MessageType = MessageType.Error, Text = $"Error in update method {ex}" },
                    Success = false
                };
            }
        }

        public GetProductResponse Get()
        {
            try
            {
                var response = new GetProductResponse();
                response.ResponseData = _products;
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                return new GetProductResponse()
                {
                    Messages = new MessageModel { MessageType = MessageType.Error, Text = $"Error in Get method {ex}" },
                    Success = false
                };
            }
        }

        public bool hasItem(string productName)
        {
            return Get().ResponseData.Any(P => P.ProductName == productName);
        }      
    }
}
