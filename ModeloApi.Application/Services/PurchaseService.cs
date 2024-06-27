using AutoMapper;
using ModeloApi.Application.DTOs.Purchase;
using ModeloApi.Application.DTOs.Validation.Purchase;
using ModeloApi.Application.Services.Interfaces;
using ModeloApi.Domain.Entities;
using ModeloApi.Domain.Interfaces;

namespace ModeloApi.Application.Services;
public class PurchaseService : IPurchaseService
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IProductRepository _productRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public PurchaseService(IPurchaseRepository purchaseRepository, IProductRepository productRepository, IPersonRepository personRepository, IMapper mapper)
    {
        _purchaseRepository = purchaseRepository;
        _productRepository = productRepository;
        _personRepository = personRepository;
        _mapper = mapper;
    }
    public async Task<ICollection<ReadPurchaseDto>> GetAllPurchasesAsync()
    {
        var purchases = await _purchaseRepository.GetAllPurchasesAsync();

        return _mapper.Map<ICollection<ReadPurchaseDto>>(purchases);
    }

    public async Task<ResultService<ReadPurchaseDto>> GetPurchaseByIdAsync(int id)
    {
        var purchase = await _purchaseRepository.GetPurchaseByIdAsync(id);

        if (purchase == null)
            return ResultService.NotFound<ReadPurchaseDto>($"Purchase id {id} not found!");

        return ResultService.Ok(_mapper.Map<ReadPurchaseDto>(purchase));
    }

    public async Task<ResultService<ReadPurchaseDto>> CreatePurchaseAsync(CreatePurchaseDto purchaseDto)
    {
        if (purchaseDto == null)
            return ResultService.Fail<ReadPurchaseDto>("Object not found!");

        var validate = new CreatePurchaseDtoValidation().Validate(purchaseDto);

        if (!validate.IsValid)
            return ResultService.RequestError<ReadPurchaseDto>("Fields validate error!", validate);

        var productId = await _productRepository.GetIdByCodErp(purchaseDto.CodErp);

        if (productId == 0)
            return ResultService.Fail<ReadPurchaseDto>($"Product with cod erp {purchaseDto.CodErp} not found!");

        var personId = await _personRepository.GetIdByDocument(purchaseDto.Document);

        if (personId == 0)
            return ResultService.Fail<ReadPurchaseDto>($"Person with documento {purchaseDto.Document} not found!");

        var purchase = new Purchase(productId, personId);

        var purchaseEntity = await _purchaseRepository.CreatePurchaseAsync(purchase);

        purchaseEntity.Product = await _productRepository.GetProductByIdAsync(productId);
        purchaseEntity.Person = await _personRepository.GetPersonByIdAsync(personId);

        return ResultService.Ok(_mapper.Map<ReadPurchaseDto>(purchaseEntity));
    }
    public async Task<ResultService> UpdatePurchaseAsync(UpdatePurchaseDto purchaseDto)
    {
        if (purchaseDto == null)
            return ResultService.Fail("Object not found!");

        var validate = new UpdatePurchaseDtoValidation().Validate(purchaseDto);

        if (!validate.IsValid)
            return ResultService.RequestError("Fields validate error!", validate);

        var productId = await _productRepository.GetIdByCodErp(purchaseDto.CodErp);

        if (productId == 0)
            return ResultService.NotFound($"Product with cod erp {purchaseDto.CodErp} not found!");

        var personId = await _personRepository.GetIdByDocument(purchaseDto.Document);

        if (personId == 0)
            return ResultService.NotFound($"Person with documento {purchaseDto.Document} not found!");

        var purchaseEntity = await _purchaseRepository.GetPurchaseByIdAsync(purchaseDto.Id);

        if (purchaseEntity == null)
            return ResultService.NotFound($"Purchase id {purchaseDto.Id} not found!");

        purchaseEntity.Update(purchaseEntity.Id, productId, personId);

        await _purchaseRepository.UpdatePurchaseAsync(purchaseEntity);

        return ResultService.Ok("Purchase updated!");
    }

    public async Task<ResultService> DeletePurchaseAsync(int id)
    {
        var purchase = await _purchaseRepository.GetPurchaseByIdAsync(id);

        if (purchase == null)
            return ResultService.NotFound($"Purchase id {id} not found!");

        await _purchaseRepository.DeletePurchaseAsync(purchase);

        return ResultService.Ok("Purchase deleted!");
    }
}
