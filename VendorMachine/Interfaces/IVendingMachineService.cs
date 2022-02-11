using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorMachine.Models;

namespace VendorMachine.Interfaces
{
    public interface IVendingMachineService
    {
        GetCoinResponse AcceptCoin(decimal Amount);       
        GetProductResponse ShowProducts();
        GetProductResponse SelectProduct(int productId, decimal AmountInMachine);
        void Execute();
    }
}
