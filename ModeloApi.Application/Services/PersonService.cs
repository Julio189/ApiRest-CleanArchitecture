
using AutoMapper;
using ModeloApi.Application.DTOs.Person;
using ModeloApi.Application.DTOs.Validation.Person;
using ModeloApi.Application.Services.Interfaces;
using ModeloApi.Domain.Entities;
using ModeloApi.Domain.Interfaces;

namespace ModeloApi.Application.Services;
public class PersonService : IPersonService
{

    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public PersonService(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }
    public async Task<ICollection<ReadPersonDto>> GetAllPeopleAsync()
    {
        var people = await _personRepository.GetAllPeopleAsync();
        return _mapper.Map<ICollection<ReadPersonDto>>(people);
    }

    public async Task<ResultService<ReadPersonDto>> GetPersonById(int id)
    {
        var personEntity = await _personRepository.GetPersonByIdAsync(id);
        if (personEntity == null)
            return ResultService.NotFound<ReadPersonDto>($"Person id {id}, not found!");
        
        return ResultService.Ok(_mapper.Map<ReadPersonDto>(personEntity));
    }

    public async Task<ResultService<ReadPersonDto>> CreatePersonAsync(CreatePersonDto createPersonDto)
    {
        if (createPersonDto == null)
            return ResultService.Fail<ReadPersonDto>("Object not found!");

        var validate = new CreatePersonDtoValidation().Validate(createPersonDto);

        if (!validate.IsValid)
            return ResultService.RequestError<ReadPersonDto>("Fields validations error", validate);

        var isDocumentAlreadyExists = await _personRepository.IsDocumentAlreadyExists(createPersonDto.Document);

        if (isDocumentAlreadyExists)
            return ResultService.Fail<ReadPersonDto>("Document is already exists!");

        var isPhoneAlreadyExists = await _personRepository.IsPhoneAlreadyExists(createPersonDto.Phone);

        if (isPhoneAlreadyExists)
            return ResultService.Fail<ReadPersonDto>("Phone is already exists!");

        var personEntity = _mapper.Map<Person>(createPersonDto);

        var data = await _personRepository.CreatePersonAsync(personEntity);

        return ResultService.Ok(_mapper.Map<ReadPersonDto>(data));
    }

    public async Task<ResultService> UpdatePersonAsync(UpdatePersonDto updatePersonDto)
    {
        if (updatePersonDto == null)
            return ResultService.Fail("Object is required!");

        var validate = new UpdatePersonDtoValidation().Validate(updatePersonDto);

        if (!validate.IsValid)
            return ResultService.RequestError("Fields validate error", validate);

        var personEntity = await _personRepository.GetPersonByIdAsync(updatePersonDto.Id);

        if (personEntity == null)
            return ResultService.NotFound($"Person id {updatePersonDto.Id} not found!");

        if (personEntity.Document != updatePersonDto.Document)
        {
            var isDocumentAlreadyExists = await _personRepository.IsDocumentAlreadyExists(updatePersonDto.Document);
            if (isDocumentAlreadyExists)
                return ResultService.Fail("Document already exists!");
        }

        if (personEntity.Phone != updatePersonDto.Phone)
        {
            var isPhoneAlreadyExists = await _personRepository.IsPhoneAlreadyExists(updatePersonDto.Phone);
            if (isPhoneAlreadyExists)
                return ResultService.Fail("Phone already exists!");
        }

        personEntity = _mapper.Map<UpdatePersonDto, Person>(updatePersonDto, personEntity);

        await _personRepository.UpdatePersonAsync(personEntity);

        return ResultService.Ok("Update sucess!");

    }

    public async Task<ResultService> DeletePersonAsync(int id)
    {
        var personEntity = await _personRepository.GetPersonByIdAsync(id);

        if (personEntity == null)
            return ResultService.NotFound($"Person id {id} not found!");

        await _personRepository.DeletePersonAsync(personEntity);

        return ResultService.Ok("Deleted sucess!");
    }
}
