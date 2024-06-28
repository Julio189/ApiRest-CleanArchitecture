using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModeloApi.Application.DTOs.Product;
using ModeloApi.Application.Services.Interfaces;

namespace ModeloApi.Api.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<ReadProductDto>>> GetAll()
    {
        var result = await _productService.GetAllProductsAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReadProductDto>> GetById(int id)
    {
        var result = await _productService.GetProductByIdAsync(id);

        if(!result.IsFound)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ReadProductDto>> Post([FromBody] CreateProductDto productDto)
    {
        var result = await _productService.CreateProductAsync(productDto);

        if (!result.IsValid)
            return BadRequest(result);

        return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] UpdateProductDto productDto)
    {
        if (id != productDto.Id)
            return BadRequest("The id in the url does not match the id in the body!");

        var result = await _productService.UpdateProductAsync(productDto);

        if (!result.IsFound)
            return NotFound(result);

        if(!result.IsValid)
            return BadRequest(result);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _productService.DeleteProductAsync(id);

        if(!result.IsFound)
            return NotFound(result);

        return NoContent();
    }

}
