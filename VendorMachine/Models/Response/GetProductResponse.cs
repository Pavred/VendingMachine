using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace VendorMachine.Models
{
    [ExcludeFromCodeCoverage]
    public class GetProductResponse : BaseResponse<List<ProductModel>>
    {
        public GetProductResponse() { }     
    }
}
