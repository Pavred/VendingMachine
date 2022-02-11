using VendorMachine.Models;

namespace VendorMachine.Interfaces
{
    public interface ICoinService
    {
        bool IsCoinValid(decimal value);
        GetCoinResponse AcceptCoins(decimal value);
    }
}