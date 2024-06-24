
using ModeloApi.Domain.Validations;

namespace ModeloApi.Domain.Entities;
public sealed class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string CodErp { get; private set; }
    public decimal Price { get; private set; }
    public ICollection<Purchase> Purchases { get; set; }

    public Product(string name, string codErp, decimal price)
    {
        Validation(name, codErp, price);
        Purchases = new List<Purchase>();
    }

    public Product(int id, string name, string codErp, decimal price)
    {
        DomainValidationException.When(id <= 0, "Invalid Id!");
        Id = id;

        Validation(name, codErp, price);
        Purchases = new List<Purchase>();
    }

    private void Validation(string name, string codErp, decimal price)
    {
        DomainValidationException.When(string.IsNullOrEmpty(name), "Name is required!");
        DomainValidationException.When(name.Length < 2, "Invalid Name! Minimum 2 characteres!");
        DomainValidationException.When(name.Length > 150, "Invalid Name! Maximum 150 characteres!");
        Name = name;

        DomainValidationException.When(string.IsNullOrEmpty(codErp), "Cod Erp is required!");
        DomainValidationException.When(codErp.Length < 3, "Invalid Cod Erp! Minimum 3 characteres!");
        CodErp = codErp;

        DomainValidationException.When(price < 0, "Invalid Price!");
        Price = price;

    }

}
