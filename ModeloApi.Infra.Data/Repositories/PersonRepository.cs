
using Microsoft.EntityFrameworkCore;
using ModeloApi.Domain.Entities;
using ModeloApi.Domain.Interfaces;
using ModeloApi.Infra.Data.Context;

namespace ModeloApi.Infra.Data.Repositories;
public class PersonRepository : IPersonRepository
{

    private readonly ApplicationDbContext _dbContext;
    public PersonRepository(ApplicationDbContext applicationDbContext)
    {
        _dbContext = applicationDbContext;
    }

    public async Task<ICollection<Person>> GetAllPeopleAsync()
    {
        return await _dbContext.People.ToListAsync();
    }

    public async Task<Person> GetPersonByIdAsync(int id)
    {
        return await _dbContext.People.FindAsync(id);
    }

    public async Task<bool> IsDocumentAlreadyExists(string document)
    {
        return await _dbContext.People.AnyAsync(x => x.Document == document);
    }

    public async Task<bool> IsPhoneAlreadyExists(string phone)
    {
        return await _dbContext.People.AnyAsync(x => x.Phone == phone); 
    }
    public async Task<int> GetIdByDocument(string document)
    {
        return (await _dbContext.People.FirstOrDefaultAsync(x => x.Document == document))?.Id ?? 0;
    }

    public async Task<Person> CreatePersonAsync(Person person)
    {
        _dbContext.Add(person);
        await _dbContext.SaveChangesAsync();
        return person;
    }
    public async Task UpdatePersonAsync(Person person)
    {
        _dbContext.Update(person);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeletePersonAsync(Person person)
    {
        _dbContext.Remove(person);
        await _dbContext.SaveChangesAsync();
    }
}
