using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorMachine.Enum;
using VendorMachine.Interfaces;
using VendorMachine.Models;

namespace VendorMachine.Services
{

    public class VendingMachineService : IVendingMachineService
    {
        public decimal totalAmountInMachine = 0;
        public bool transactionComplete = false;
        private readonly ICoinService _coinService;
        private readonly IProductService _productService;

        public VendingMachineService(ICoinService coinService, IProductService productService)
        {
            _coinService = coinService;
            _productService = productService;
        }

        public GetCoinResponse AcceptCoin(decimal Amount)
        {
            var response = new GetCoinResponse();

            response = _coinService.AcceptCoins(Amount);
            if (response.Success)
                totalAmountInMachine = response.ResponseData.Value;
            return response;
        }

        public GetProductResponse ShowProducts()
        {
            return _productService.GetAllProduct();
        }

        public GetProductResponse SelectProduct(int productId, decimal AmountInMachine)
        {
            var response = new GetProductResponse();
            if (productId == 0)
            {
                response.Messages =
                       new MessageModel { MessageType = MessageType.Error, Text = "Select Product" }
                    ;
                response.Success = false;
                return response;
            }
            //no coins entered, but selection pressed
            if (AmountInMachine == 0)
            {
                //if exact change item, message = "exact change only"
                response.Messages =
                       new MessageModel { MessageType = MessageType.Error, Text = "Insert Coin" }
                    ;
                response.Success = false;
                return response;
            }
            var product = _productService.GetProductById(productId);

            if (product == null)
            {
                response.Messages =
                       new MessageModel
                       {
                           MessageType = MessageType.Error,
                           Text = "Item not in list,Try again"
                       };
                response.Success = false;
                return response;
            }

            //entered coins less than cost
            if (AmountInMachine < product.Price)
            {
                response.Messages =
                       new MessageModel
                       {
                           MessageType = MessageType.Error,
                           Text = $"Price : {product.Price}"
                       };
                response.Success = false;
                return response;
            }

            //all good, valid product and valid amount entered
            var quantity = _productService.GetProductStock(product.ProductName);

            if (quantity > 0)
            {
                product.Quantity = quantity--;
                _productService.UpdateQuantity(product);
                response.Messages = new MessageModel
                {
                    MessageType = MessageType.None,
                    Text = $"THANK YOU ! Please take your change: {totalAmountInMachine - product.Price}"
                };
                transactionComplete = true;
                response.Success = true;
                return response;
            }

            response.Messages = new MessageModel { MessageType = MessageType.None, Text = "Sold Out" };
            response.Success = false;
            return response;
        }

        public void Execute()
        {
            while (transactionComplete == false)
            {
                BuildMainMenu();
                int selectedOption = Convert.ToInt32(Console.ReadLine());

                switch (selectedOption)
                {
                    case 1:
                        InsertCoin();
                        break;
                    case 2:
                        DisplayProducts();
                        break;
                    case 3:
                        SelectProduct();
                        break;

                    case 4:
                    default:
                        transactionComplete = true;
                        Console.WriteLine("Insert Coin :  ");
                        break;
                }
            }

            Console.WriteLine("Press any to exit");
        }

        [ExcludeFromCodeCoverage]
        private static void BuildMainMenu()
        {
            Console.WriteLine("Welcome!!");
            Console.WriteLine("------------------------------------");

            Console.WriteLine("Select 1. Insert coin 2. Show Product  3. Select product  4. Exit");
        }

        [ExcludeFromCodeCoverage]
        private void SelectProduct()
        {
            if (totalAmountInMachine == 0)
                InsertCoin();
            Console.WriteLine($"SELECT: {String.Concat(ShowProducts().ResponseData.Select(o => o.ProductId + " " + o.ProductName + " for £" + o.Price + "  \n"))}");
            var selectedProduct = Convert.ToInt32(Console.ReadLine());
            var result = SelectProduct(selectedProduct, totalAmountInMachine);
            Console.WriteLine(result.Messages.Text);
        }

        [ExcludeFromCodeCoverage]
        private void DisplayProducts()
        {
            var products = ShowProducts();
            Console.WriteLine($"{String.Concat(products.ResponseData.Select(o => o.ProductName + " " + o.Price + " " + o.Quantity + " left \n"))}");
        }

        [ExcludeFromCodeCoverage]
        private void InsertCoin()
        {
            Console.WriteLine("Insert Coin :  ");
            decimal value = Convert.ToDecimal(Console.ReadLine());
            var response = AcceptCoin(value);
            Console.WriteLine(response.Messages.Text);
        }
    }
}
