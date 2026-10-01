using Controller_Service_Repository.Models.DTOs.Requests;
using Controller_Service_Repository.Models.DTOs.Responses;

namespace Controller_Service_Repository.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductForReadDto> GetAllProducts();
        ProductForReadDto? GetProductById(int id);
        ProductForReadDto CreateProduct(ProductForCreateDto dto);
        void UpdateProduct(int id, ProductForUpdateDto dto);
        void DeleteProduct(int id);

    }
}
