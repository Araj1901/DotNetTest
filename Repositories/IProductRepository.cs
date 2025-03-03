using CartManagementMVC.Models;

namespace CartManagementMVC.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetProductList();
        Product GetProductById(int productId);
        bool SaveProduct(Product product);
    }
}
