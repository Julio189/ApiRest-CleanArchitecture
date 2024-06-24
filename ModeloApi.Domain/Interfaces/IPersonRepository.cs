
using ModeloApi.Domain.Entities;

namespace ModeloApi.Domain.Interfaces;
public interface IPersonRepository
{
    Task<ICollection<Person>> GetAllPeopleAsync();
    Task<Person> GetPersonByIdAsync(int id);
    Task<bool> IsDocumentAlreadyExists(string document);
    Task<bool> IsPhoneAlreadyExists(string phone);
    Task<int> GetIdByDocument(string document);
    Task<Person> CreatePersonAsync(Person person);
    Task UpdatePersonAsync(Person person);
    Task DeletePersonAsync(Person person);
}
