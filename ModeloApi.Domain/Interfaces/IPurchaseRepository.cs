
using ModeloApi.Domain.Entities;

namespace ModeloApi.Domain.Interfaces;
public interface IPurchaseRepository
{
    Task<ICollection<Purchase>> GetAllPurchasesAsync();
    Task<ICollection<Purchase>> GetPurchaseByProductIdAsync(int productId);
    Task<ICollection<Purchase>> GetPurchaseByPersonId(int personId);
    Task<Purchase> GetPurchaseByIdAsync(int id);
    Task<Purchase> CreatePurchaseAsync(Purchase purchase);
    Task UpdatePurchaseAsync(Purchase purchase);
    Task DeletePurchaseAsync(Purchase purchase);

}
