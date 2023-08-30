using Back_End.Models;

namespace Back_End.IServices
{
    public interface IProductService
    {
        Task BuyProduct(Purchase purchase);
        Task<Product> GetProductById(int id);
        Task<List<Product>> GetProducts(int id);
        Task<List<Product>> GetMyProducts(int id);
        Task RegisterProduct(Product product);
        Task SetHideProduct(Product product);
        Task<List<Purchase>> GetPurchases(int id);
        Task<Product> GetProductDetails(int idproduct, int iduser);
        Task<Product> SeeeEditProduct(int idproduct, int iduser);
       
    }
}
