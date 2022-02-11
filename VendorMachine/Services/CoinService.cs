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
    public class CoinService : ICoinService
    {
        decimal totalAmount = 0;
        private readonly IEnumerable<CoinModel> AcceptedCoins = new List<CoinModel>();
        public CoinService()
        {
            AcceptedCoins = new List<CoinModel>() {
                new CoinModel() { CoinType = ValidCoinTypes.FiveCents, Value = 0.05M},
                new CoinModel() { CoinType = ValidCoinTypes.TenCents, Value = 0.10M},
                new CoinModel() { CoinType = ValidCoinTypes.TwentyCents, Value = 0.20M},
                new CoinModel() { CoinType = ValidCoinTypes.FiftyCents, Value = 0.50M},
                new CoinModel() { CoinType = ValidCoinTypes.OneEuro, Value = 1.00M},
                new CoinModel() { CoinType = ValidCoinTypes.TwoEuro, Value = 2.00M} };
        }

        public bool IsCoinValid(decimal value)
        {
            return AcceptedCoins.Any(c => c.Value == value);
        }

        public GetCoinResponse AcceptCoins(decimal value)
        {
            var response = new GetCoinResponse();
            if (value == 0)
            {
                return new GetCoinResponse()
                {
                    Messages =
                    new MessageModel { MessageType = MessageType.Error, Text = $"Input Coin" },
                    Success = false
                };
            }

            if (IsCoinValid(value))
            {
                totalAmount = value + totalAmount;
                response.ResponseData = new CoinModel
                {
                    Value = totalAmount
                };

                response.Success = true;
                response.Messages =
                    new MessageModel
                    {
                        MessageType = MessageType.None,
                        Text = $"Amount entered: {value} , Total Amount = {totalAmount}"
                    };
                return response;
            }

            return new GetCoinResponse()
            {
                Messages =
                    new MessageModel { MessageType = MessageType.Error, Text = $"Insert valid Coin" }
                ,
                Success = false

            };

        }
    }
}
