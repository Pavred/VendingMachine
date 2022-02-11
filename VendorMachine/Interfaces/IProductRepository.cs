using VendorMachine.Models;

namespace VendorMachine.Interfaces
{
    public interface IProductRepository
    {
        GetProductResponse Add(ProductModel product);        
        GetProductResponse Update(ProductModel product);
        GetProductResponse Get();
    }
}