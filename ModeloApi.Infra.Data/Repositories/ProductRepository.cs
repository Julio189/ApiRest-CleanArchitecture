
using Microsoft.EntityFrameworkCore;
using ModeloApi.Domain.Entities;
using ModeloApi.Domain.Interfaces;
using ModeloApi.Infra.Data.Context;

namespace ModeloApi.Infra.Data.Repositories;
public class ProductRepository : IProductRepository
{

    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext applicationDbContext)
    {
        _dbContext = applicationDbContext;
    }

    public async Task<ICollection<Product>> GetAllProductsAsync()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _dbContext.Products.FindAsync(id);
    }
    public async Task<bool> IsNameAlreadyExists(string name)
    {
        return await _dbContext.Products.AnyAsync(x => x.Name.ToLower().Equals(name.ToLower()));
    }
    public async Task<bool> IsCodErpAlreadyExists(string codedErp)
    {
        return await _dbContext.Products.AnyAsync(x => x.CodErp.ToLower().Equals(codedErp.ToLower()));
    }
    public async Task<int> GetIdByCodErp(string codedErp)
    {
        return (await _dbContext.Products.FirstOrDefaultAsync(x => x.CodErp == codedErp))?.Id ?? 0;
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        _dbContext.Add(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }
    public async Task UpdateProductAsync(Product product)
    {
        _dbContext.Update(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(Product product)
    {
        _dbContext.Remove(product);
        await _dbContext.SaveChangesAsync();
    }
}
