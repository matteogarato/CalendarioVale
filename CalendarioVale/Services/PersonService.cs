using CalendarioVale.Data.Model;
using CalendarioVale.Data.Reposotory;

namespace CalendarioVale.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Person?> GetById(string id)
    {
        return await _personRepository.GetById(id).ConfigureAwait(false);
    }

    public async Task Save(Person toSave)
    {
        await _personRepository.Save(toSave).ConfigureAwait(false);
    }

    public IEnumerable<Person> GetAllPerson()
    {
        return _personRepository.GetAllPerson();
    }
}