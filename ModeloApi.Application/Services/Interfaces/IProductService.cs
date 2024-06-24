
using ModeloApi.Application.DTOs.Product;

namespace ModeloApi.Application.Services.Interfaces;
public interface IProductService
{
    Task<ICollection<ReadProductDto>> GetAllProductsAsync();
    Task<ResultService<ReadProductDto>> GetProductByIdAsync(int id);
    Task<ResultService<ReadProductDto>> CreateProductAsync(CreateProductDto productDto);
    Task<ResultService> UpdateProductAsync(UpdateProductDto productDto);
    Task<ResultService> DeleteProductAsync(int id);
}
