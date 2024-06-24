
using ModeloApi.Application.DTOs.Person;

namespace ModeloApi.Application.Services.Interfaces;
public interface IPersonService
{
    Task<ICollection<ReadPersonDto>> GetAllPeopleAsync();
    Task<ResultService<ReadPersonDto>> GetPersonById(int id);
    Task<ResultService<ReadPersonDto>> CreatePersonAsync(CreatePersonDto createPersonDto);
    Task<ResultService> UpdatePersonAsync(UpdatePersonDto updatePersonDto);
    Task<ResultService> DeletePersonAsync(int id);
    //Task<ResultService<ReadPersonDto>> IsDocumentAlreadyExists(string document); 
    //Task<ResultService<ReadPersonDto>> IsPhoneAlreadyExists(string phone); 
}
