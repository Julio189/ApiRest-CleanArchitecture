
namespace ModeloApi.Application.DTOs.Product;
public class UpdateProductDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? CodErp { get; set; }
    public decimal Price { get; set; }
}
