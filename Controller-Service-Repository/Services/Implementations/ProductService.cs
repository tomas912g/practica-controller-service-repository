using Controller_Service_Repository.Entities;
using Controller_Service_Repository.Models.DTOs.Requests;
using Controller_Service_Repository.Models.DTOs.Responses;
using Controller_Service_Repository.Repositories.Implementations;

namespace Controller_Service_Repository.Services.Implementations
{
    public class ProductService
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
            var newProduct = new Product();
            newProduct.Name = dto.Name;
            newProduct.Price = dto.Price;

            _repository.AddProduct(newProduct);

            var productToDto = new ProductForReadDto();
            productToDto.Id = newProduct.Id;
            productToDto.Name = newProduct.Name;
            productToDto.Price = newProduct.Price;

            return productToDto;
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
    }
}
