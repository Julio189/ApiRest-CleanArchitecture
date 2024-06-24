
using ModeloApi.Domain.Entities;

namespace ModeloApi.Domain.Interfaces;
public interface IProductRepository
{
    Task<ICollection<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(int id);
    Task<bool> IsNameAlreadyExists(string name);
    Task<bool> IsCodErpAlreadyExists(string codedErp);
    Task<int> GetIdByCodErp(string codedErp);
    Task<Product> CreateProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(Product product);
}
