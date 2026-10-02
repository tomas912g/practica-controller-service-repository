using Controller_Service_Repository.Entities;
using Controller_Service_Repository.Models.DTOs.Requests;
using Controller_Service_Repository.Models.DTOs.Responses;
using Controller_Service_Repository.Repositories.Implementations;
using Controller_Service_Repository.Services.Interfaces;

namespace Controller_Service_Repository.Services.Implementations
{
    public class ProductService : IProductService
    {
        private ProductRepository _repository = new ProductRepository();
        public List<ProductForReadDto> GetAllProducts()
        { 
            var products = _repository.GetAllProducts();
            var dtos = new List<ProductForReadDto>();

            foreach (var product in products)
            {
                var dto = new ProductForReadDto();
                dto.Id = product.Id;
                dto.Name = product.Name;
                dto.Price = product.Price;
                dtos.Add(dto);
            }
            return dtos;
        }
        public ProductForReadDto? GetProductById(int id)
        {
            var product = _repository.GetProductById(id);
            if(product == null)
            {
                return null;
            }
            var dto = new ProductForReadDto();
            dto.Id = product.Id;
            dto.Name = product.Name;
            dto.Price = product.Price;

            return dto;           
        }
        public ProductForReadDto CreateProduct(ProductForCreateDto dto)
        {
            var products = _repository.GetAllProducts();
            if (products.Any(p => p.Name.ToLower() == dto.Name.ToLower()))
            {
                return null; 
            }
            var newProduct = new Product();
            newProduct.Name = dto.Name;
            newProduct.Price = dto.Price;

            _repository.AddProduct(newProduct);

            var responseDto = new ProductForReadDto();
            responseDto.Id = newProduct.Id;
            responseDto.Name = newProduct.Name;
            responseDto.Price = newProduct.Price;

            return responseDto;
        }
        public void UpdateProduct(int id, ProductForUpdateDto dto)
        {
            var productExiste = _repository.GetProductById(id);
            if(productExiste == null)
            {
                return;
            }

            var productUpdate = new Product();
            productUpdate.Id = id;
            productUpdate.Name = dto.Name;
            productUpdate.Price = dto.Price;

            _repository.UpdateProduct(productUpdate);
        }
        public void DeleteProduct(int id)
        {
            var productExiste = _repository.GetProductById(id);
            if(productExiste == null)
            {
                return;
            }
            _repository.DeleteProduct(productExiste);
        }
        public List<ProductForReadDto> SearchProductsByName(string name)
        {
            var products = _repository.SearchProductsByName(name);
            var dtos = new List<ProductForReadDto>();
            foreach (var product in products)
            {
                var dto = new ProductForReadDto();
                dto.Id = product.Id;
                dto.Name = product.Name;
                dto.Price = product.Price;
                dtos.Add(dto);
            }
            return dtos;
        }
        public ProductStatsDto GetStats()
        {
            var products = _repository.GetAllProducts();
            var stats = new ProductStatsDto();
            if(products.Count == 0)
            {
                stats.Total = 0;
                stats.AveragePrice = 0;
                stats.MostExpensiveName = "";
                return stats;
            }
            stats.Total = products.Count();
            stats.AveragePrice = products.Average(p => p.Price);
            stats.MostExpensiveName = products.OrderByDescending(p => p.Price).First().Name;

            return stats;
        }
    }
}
