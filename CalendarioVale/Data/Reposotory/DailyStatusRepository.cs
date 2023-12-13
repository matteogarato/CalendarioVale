using CalendarioVale.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CalendarioVale.Data.Reposotory;

public class DailyStatusRepository : IDailyStatusRepository
{
    private readonly ApplicationDbContext _context;

    public DailyStatusRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DailyStatus?> GetByDateAndPerson(DateTime date, Person person)
    {
        return await GetByDateCompiled(_context, date, person).ConfigureAwait(false);
    }

    public IAsyncEnumerable<DailyStatus> GetBetweenDate(DateTime startDate, DateTime endDate, Person person = null)
    {
        return person == null ? GetBetweenDateCompiled(_context, startDate, endDate) : GetBetweenDateAndPersonCompiled(_context, startDate, endDate, person);
    }

    public async Task Save(DailyStatus toSave)
    {
        if (toSave != null)
        {
            toSave.Modify = DateTime.Now;
            var status = await GetByDateAndPerson(toSave.Date, toSave.Person).ConfigureAwait(false);
            if (status is not null)
            {
                status.Biometrics = toSave.Biometrics;
                status.Person = toSave.Person;
                status.Note = toSave.Note;
                status.Modify = toSave.Modify;
                status.Visible = toSave.Visible;
            }
            else
            {
                await _context.AddAsync(toSave).ConfigureAwait(false);
            }
            _context.SaveChanges();
        }
    }

    private static readonly Func<ApplicationDbContext, DateTime, Person, Task<DailyStatus?>> GetByDateCompiled =
   EF.CompileAsyncQuery(
        (ApplicationDbContext context, DateTime date, Person person) =>
           context.Set<DailyStatus>().Include(b => b.Biometrics).Include(p => p.Person).FirstOrDefault(h => h.Visible && h.Date.Year == date.Year && h.Date.Month == date.Month && h.Date.Day == date.Day && h.Person != null && h.Person.Id == person.Id));

    private static readonly Func<ApplicationDbContext, DateTime, DateTime, IAsyncEnumerable<DailyStatus>> GetBetweenDateCompiled =
    EF.CompileAsyncQuery(
         (ApplicationDbContext context, DateTime startDate, DateTime endDate) =>
            context.Set<DailyStatus>().Include(b => b.Biometrics).Include(p => p.Person).Where(h => h.Visible && h.Date >= startDate && h.Date <= endDate));

    private static readonly Func<ApplicationDbContext, DateTime, DateTime, Person, IAsyncEnumerable<DailyStatus>> GetBetweenDateAndPersonCompiled =
    EF.CompileAsyncQuery(
         (ApplicationDbContext context, DateTime startDate, DateTime endDate, Person person) =>
            context.Set<DailyStatus>().Include(b => b.Biometrics).Include(p => p.Person).Where(h => h.Visible && h.Date >= startDate && h.Date <= endDate && h.Person != null && h.Person.Id == person.Id));
}