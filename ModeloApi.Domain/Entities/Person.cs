
using ModeloApi.Domain.Validations;

namespace ModeloApi.Domain.Entities;
public sealed class Person
{
    public int Id { get; private set; }
    public string? Name { get; private set; }
    public string? Document { get; private set; }
    public string? Phone { get; private set; }
    public ICollection<Purchase> Purchases { get; set; }

    public Person(string name, string document, string phone)
    {
        Validation(name, document, phone);
        Purchases = new List<Purchase>();
    }

    public Person(int id, string name, string document, string phone)
    {
        DomainValidationException.When(id <= 0, "Invalid Id!");
        Id = id;

        Validation(name, document, phone);
        Purchases = new List<Purchase>();
    }


    private void Validation(string name, string document, string phone)
    {
        DomainValidationException.When(string.IsNullOrEmpty(name), "Name is required!");
        DomainValidationException.When(name.Length < 3, "Invalid Name! Minimum 3 characteres!");
        DomainValidationException.When(name.Length > 150, "Invalid Name! Maximum 150 characteres!");
        Name = name;

        DomainValidationException.When(string.IsNullOrEmpty(document), "Document is required!");
        DomainValidationException.When(document.Length != 11, "Invalid Document! Document must have 11 characteres!");
        Document = document;

        DomainValidationException.When(string.IsNullOrEmpty(phone), "Phone is required!");
        DomainValidationException.When(phone.Length < 11, "Invalid Phone! Minimum 11 characteres!");
        DomainValidationException.When(phone.Length > 13, "Invalid Phone! Maximum 13 characteres!");
        Phone = phone;
    }

}
