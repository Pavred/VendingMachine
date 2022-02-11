using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorMachine.Models
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseResponse<T>
    {
        protected BaseResponse()
        {
          
        }

        public MessageModel Messages { get; set; }
        public bool Success { get; set; }
        public T ResponseData  { get; set; }

}
}
