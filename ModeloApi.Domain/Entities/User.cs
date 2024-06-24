
using ModeloApi.Domain.Validations;

namespace ModeloApi.Domain.Entities;
public sealed class User
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Password { get; private set; }
    public string Group { get; private set; }
    public bool Status { get; private set; }

    public User(string name, string password, string group)
    {
        Validation(name, password, group);
        Status = true;
    }

    public User(int id, string name, string password, string group, bool status)
    {
        DomainValidationException.When(id <= 0, "Invalid id! Id must be greater than 0!");
        Id = id;
        Validation(name, password, group);
        Status = status;
    }

    public void UpdatePassword(string password)
    {
        Password = password;
    }

    private void Validation(string name, string password, string group)
    {
        DomainValidationException.When(string.IsNullOrEmpty(name), "Name is required!");
        DomainValidationException.When(name.Length < 3, "Invalid name! Minimum 3 characteres!");
        Name = name;

        DomainValidationException.When(string.IsNullOrEmpty(password), "Password is required!");
        DomainValidationException.When(password.Length < 3, "Invalid password! Minimum 3 characteres!");
        Password = password;

        DomainValidationException.When(string.IsNullOrEmpty(group), "Group is required!");
        DomainValidationException.When(group.Length < 3, "Invalid group! Minimum 3 characteres!");
        Group = group;
    }
}
