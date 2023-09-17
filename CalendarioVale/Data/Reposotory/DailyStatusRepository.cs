using CalendarioVale.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CalendarioVale.Data.Reposotory
{
    public class DailyStatusRepository : IDailyStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public DailyStatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DailyStatus> GetByDate(DateTime date)
        {
            return await GetByDateCompiled(_context, date);
        }

        public IAsyncEnumerable<DailyStatus> GetBetweenDate(DateTime startDate, DateTime endDate)
        {
            return GetBetweenDateCompiled(_context, startDate, endDate);
        }

        public async Task Save(DailyStatus toSave)
        {
            if (toSave != null)
            {
                toSave.Modify = DateTime.Now;
                if (_context.DailyStatuses.Any(d => d.Date == toSave.Date))
                {
                    var status = await GetByDate(toSave.Date);
                    status.GotoWork = toSave.GotoWork;
                    status.Note = toSave.Note;
                    status.Modify = toSave.Modify;
                    status.Visible = toSave.Visible;
                }
                else
                {
                    await _context.AddAsync(toSave);
                }
                _context.SaveChanges();
            }
        }

        private static readonly Func<ApplicationDbContext, DateTime, Task<DailyStatus?>> GetByDateCompiled =
       EF.CompileAsyncQuery(
            (ApplicationDbContext context, DateTime date) =>
               context.Set<DailyStatus>().Include(b => b.Biometrics).FirstOrDefault(h => h.Date == date && h.Visible == true));

        private static readonly Func<ApplicationDbContext, DateTime, DateTime, IAsyncEnumerable<DailyStatus>> GetBetweenDateCompiled =
        EF.CompileAsyncQuery(
             (ApplicationDbContext context, DateTime startDate, DateTime endDate) =>
                context.Set<DailyStatus>().Include(b => b.Biometrics).Where(h => h.Visible == true && h.Date >= startDate && h.Date <= endDate));
    }
}