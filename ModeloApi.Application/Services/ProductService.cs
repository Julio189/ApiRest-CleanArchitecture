
using AutoMapper;
using ModeloApi.Application.DTOs.Product;
using ModeloApi.Application.DTOs.Validation.Person;
using ModeloApi.Application.DTOs.Validation.Product;
using ModeloApi.Application.Services.Interfaces;
using ModeloApi.Domain.Entities;
using ModeloApi.Domain.Interfaces;

namespace ModeloApi.Application.Services;
public class ProductService : IProductService
{

    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<ReadProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllProductsAsync();

        return _mapper.Map<ICollection<ReadProductDto>>(products);
    }

    public async Task<ResultService<ReadProductDto>> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);

        if (product == null)
            return ResultService.NotFound<ReadProductDto>($"Product id {id} not found!");

        return ResultService.Ok(_mapper.Map<ReadProductDto>(product));  
    }
    public async Task<ResultService<ReadProductDto>> CreateProductAsync(CreateProductDto productDto)
    {
        if (productDto == null)
            return ResultService.Fail<ReadProductDto>("Object not found!");

        var validate = new CreateProductDtoValidation().Validate(productDto);

        if (!validate.IsValid)
            return ResultService.RequestError<ReadProductDto>("Fields validate error!", validate);

        var isNameAlreadyExists = await _productRepository.IsNameAlreadyExists(productDto.Name);

        if (isNameAlreadyExists)
            return ResultService.Fail<ReadProductDto>("Name already exists!");

        var isCodigoErpAlreadyExists = await _productRepository.IsCodErpAlreadyExists(productDto.CodErp);

        if (isCodigoErpAlreadyExists)
            return ResultService.Fail<ReadProductDto>("CodErp already exists!");

        var productEntity = _mapper.Map<Product>(productDto);

        await _productRepository.CreateProductAsync(productEntity);

        return ResultService.Ok(_mapper.Map<ReadProductDto>(productEntity));

    }
    public async Task<ResultService> UpdateProductAsync(UpdateProductDto productDto)
    {
        if (productDto == null)
            return ResultService.Fail("Object not found!");

        var validate = new UpdateProductDtoValidation().Validate(productDto);

        if (!validate.IsValid)
            return ResultService.RequestError("Fields validate error!", validate);

        var productEntity = await _productRepository.GetProductByIdAsync(productDto.Id);

        if (productEntity == null)
            return ResultService.NotFound($"Product id {productDto.Id} not found!");

        if(productEntity.Name != productDto.Name)
        {
            var isNameAlreadyExists = await _productRepository.IsNameAlreadyExists(productDto.Name);

            if (isNameAlreadyExists)
                return ResultService.Fail("Name already exists!");
        }

        if(productEntity.CodErp != productDto.CodErp)
        {
            var isCodErpAlreadyExists = await _productRepository.IsCodErpAlreadyExists(productDto.CodErp);

            if (isCodErpAlreadyExists)
                return ResultService.Fail("CodErp already exists!");
        }    

        productEntity = _mapper.Map(productDto, productEntity);

        await _productRepository.UpdateProductAsync(productEntity);

        return ResultService.Ok("Product Updated!");

    }

    public async Task<ResultService> DeleteProductAsync(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);

        if (product == null)
            return ResultService.NotFound($"Product id {id} not found!");

        await _productRepository.DeleteProductAsync(product);

        return ResultService.Ok("Product Deleted!");
    }
}
