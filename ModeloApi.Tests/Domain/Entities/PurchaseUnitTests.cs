
using FluentAssertions;
using ModeloApi.Domain.Entities;
using ModeloApi.Domain.Validations;

namespace ModeloApi.Tests.Domain.Entities;
public class PurchaseUnitTests
{
    [Fact(DisplayName = "Create Purchase with valid state")]
    public void CreatePerson_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Purchase(1, 1);
        action.Should().NotThrow<DomainValidationException>();
    }

    [Fact(DisplayName = "Create Purchase with nagative product id")]
    public void CreatePerson_WithNegativeProductId_DomainExceptionInvalidProductId()
    {
        Action action = () => new Purchase(-1, 1);
        action.Should().Throw<DomainValidationException>().WithMessage("Invalid product id!");
    }

    [Fact(DisplayName = "Create Purchase with nagative person id")]
    public void CreatePerson_WithNegativePersonId_DomainExceptionInvalidPersonId()
    {
        Action action = () => new Purchase(1, -1);
        action.Should().Throw<DomainValidationException>().WithMessage("Invalid person id!");
    }
}
