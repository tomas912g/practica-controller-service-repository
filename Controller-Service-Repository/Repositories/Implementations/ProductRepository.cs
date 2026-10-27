using Controller_Service_Repository.Data;
using Controller_Service_Repository.Entities;
using Controller_Service_Repository.Repositories.Interfaces;

namespace Controller_Service_Repository.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }
        public Product? GetProductById (int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id); 
        }
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void UpdateProduct(Product product)
        {
            var oldProduct = _context.Products.First(p => p.Id == product.Id);
            oldProduct.Name = product.Name;
            oldProduct.Price = product.Price;
            _context.SaveChanges();
        }
        public void DeleteProduct(Product product)
        {
            var oldProduct = _context.Products.First(p => p.Id == product.Id);
            _context.Products.Remove(oldProduct);
            _context.SaveChanges();
        }
        public List<Product> SearchProductsByName(string name)
        {
            return _context.Products
                .Where(p => p.Name.ToLower().Contains(name.ToLower()))
                .ToList();
        }
    }
}
