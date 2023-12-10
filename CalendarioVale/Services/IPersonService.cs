using CalendarioVale.Data.Model;

namespace CalendarioVale.Services;

public interface IPersonService
{
    public Task<Person?> GetById(string id);

    public Task Save(Person toSave);

    public IEnumerable<Person> GetAllPerson();
}