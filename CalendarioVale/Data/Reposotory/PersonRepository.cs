using Microsoft.EntityFrameworkCore;

using CalendarioVale.Data.Model;

namespace CalendarioVale.Data.Reposotory;

public class PersonRepository : IPersonRepository
{
    private readonly ApplicationDbContext _context;

    public PersonRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Person?> GetById(string id)
    {
        return await GetByIdCompiled(_context, id).ConfigureAwait(false);
    }

    public async Task Save(Person toSave)
    {
        if (toSave != null)
        {
            var dbPerson = await GetById(toSave.Id).ConfigureAwait(false);
            if (dbPerson is not null)
            {
                dbPerson.Name = toSave.Name;
                dbPerson.Surname = toSave.Surname;
                dbPerson.BirthDate = toSave.BirthDate;
                dbPerson.EditDate = DateTime.Now;

            }
            else
            {
                toSave.CreationDate = DateTime.Now;
                await _context.AddAsync(toSave).ConfigureAwait(false);
            }
            _context.SaveChanges();
        }
    }

    public IEnumerable<Person> GetAllPerson()
    {
        return _context.Persons;
    }

    private static readonly Func<ApplicationDbContext, string, Task<Person?>> GetByIdCompiled =
   EF.CompileAsyncQuery(
        (ApplicationDbContext context, string id) =>
           context.Set<Person>().FirstOrDefault(p => p.Id == id));

}