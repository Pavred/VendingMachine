using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorMachine.Enum;

namespace VendorMachine.Models
{
    [ExcludeFromCodeCoverage]
    public class CoinModel
    {
        public ValidCoinTypes CoinType { get;  set; }

        public decimal Value { get;  set; }

    }
}

