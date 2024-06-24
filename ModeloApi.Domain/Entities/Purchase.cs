
using ModeloApi.Domain.Validations;

namespace ModeloApi.Domain.Entities;
public sealed class Purchase
{
    public int Id { get; private set; }
    public int ProductId { get; private set; }
    public int PersonId { get; private set; }
    public DateTime? Date { get; set; }
    public Product? Product { get; set; }
    public Person? Person { get; set; }

    public Purchase(int productId, int personId)
    {
        Validation(productId, personId);

        Date = DateTime.UtcNow;
    }

    public Purchase(int id, int productId, int personId, DateTime date)
    {
        DomainValidationException.When(id <= 0, "Invalid Id!");
        Id = id;

        Date = date;

        Validation(productId, personId);
    }

    public void Update(int id, int productId, int personId)
    {
        DomainValidationException.When(id <= 0, "Invalid Id!");
        Id = id;
        Validation(productId, personId);

    }

    private void Validation(int productId, int personId)
    {
        DomainValidationException.When(productId <= 0, "Invalid product id!");
        ProductId = productId;

        DomainValidationException.When(personId <= 0, "Invalid person id!");
        PersonId = personId;
    }


}
