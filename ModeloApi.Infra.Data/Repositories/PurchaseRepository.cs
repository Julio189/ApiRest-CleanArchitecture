
using Microsoft.EntityFrameworkCore;
using ModeloApi.Domain.Entities;
using ModeloApi.Domain.Interfaces;
using ModeloApi.Infra.Data.Context;

namespace ModeloApi.Infra.Data.Repositories;
public class PurchaseRepository : IPurchaseRepository
{

    private readonly ApplicationDbContext _dbContext;

    public PurchaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ICollection<Purchase>> GetAllPurchasesAsync()
    {
        return await _dbContext.Purchases
            .Include(x => x.Product)
            .Include(x => x.Person).ToListAsync();
    }

    public async Task<Purchase> GetPurchaseByIdAsync(int id)
    {
        return await _dbContext.Purchases
            .Include(x => x.Product)
            .Include(x => x.Person)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<ICollection<Purchase>> GetPurchaseByProductIdAsync(int productId)
    {
        return await _dbContext.Purchases
            .Include(x => x.Product)
            .Include(x => x.Person)
            .Where(x => x.ProductId == productId).ToListAsync();
    }

    public async Task<ICollection<Purchase>> GetPurchaseByPersonId(int personId)
    {
        return await _dbContext.Purchases
            .Include(x => x.Person)
            .Include (x => x.Product)
            .Where(x => x.PersonId == personId).ToListAsync();
    }

    public async Task<Purchase> CreatePurchaseAsync(Purchase purchase)
    {
        _dbContext.Add(purchase);
        await _dbContext.SaveChangesAsync();
        return purchase;
    }
    public async Task UpdatePurchaseAsync(Purchase purchase)
    {
        _dbContext.Update(purchase);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeletePurchaseAsync(Purchase purchase)
    {
        _dbContext.Remove(purchase);
        await _dbContext.SaveChangesAsync();
    }

    
}
