using VendorMachine.Models;

namespace VendorMachine.Interfaces
{
    public interface IProductService
    {
        GetProductResponse AddProducts(ProductModel product);  
       
        ProductModel GetProduct(string productName);
        int GetProductStock(string productName);
        GetProductResponse UpdateQuantity(ProductModel product);
        GetProductResponse GetAllProduct();
        ProductModel GetProductById(int productId);
    }
}