using CalendarioVale.Data.Model;

namespace CalendarioVale.Data.Reposotory;

public interface IPersonRepository
{
    Task<Person?> GetById(string id);

    Task Save(Person toSave);

    IEnumerable<Person> GetAllPerson();
}