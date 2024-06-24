
using ModeloApi.Application.DTOs.Purchase;
using ModeloApi.Domain.Interfaces;

namespace ModeloApi.Application.Services.Interfaces;
public interface IPurchaseService
{
   Task<ICollection<ReadPurchaseDto>> GetAllPurchasesAsync();
    Task<ResultService<ReadPurchaseDto>> GetPurchaseByIdAsync(int id);
    Task<ResultService<ReadPurchaseDto>> CreatePurchaseAsync(CreatePurchaseDto purchaseDto);
    Task<ResultService> UpdatePurchaseAsync(UpdatePurchaseDto purchaseDto);
    Task<ResultService> DeletePurchaseAsync(int id);
}
