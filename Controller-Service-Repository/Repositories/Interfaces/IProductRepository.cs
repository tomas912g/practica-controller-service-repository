using Controller_Service_Repository.Entities;

namespace Controller_Service_Repository.Repositories.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product? GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);

    }
}
