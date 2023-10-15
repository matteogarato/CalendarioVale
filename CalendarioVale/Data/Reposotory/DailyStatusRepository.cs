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

        public async Task<DailyStatus?> GetByDate(DateTime date)
        {
            return await GetByDateCompiled(_context, date).ConfigureAwait(false);
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
                var status = await GetByDate(toSave.Date).ConfigureAwait(false);
                if (status is not null)
                {
                    status.GotoWork = toSave.GotoWork;
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

        private static readonly Func<ApplicationDbContext, DateTime, Task<DailyStatus?>> GetByDateCompiled =
       EF.CompileAsyncQuery(
            (ApplicationDbContext context, DateTime date) =>
               context.Set<DailyStatus>().Include(b => b.Biometrics).FirstOrDefault(h => h.Visible && h.Date.Year == date.Year && h.Date.Month == date.Month && h.Date.Day == date.Day));

        private static readonly Func<ApplicationDbContext, DateTime, DateTime, IAsyncEnumerable<DailyStatus>> GetBetweenDateCompiled =
        EF.CompileAsyncQuery(
             (ApplicationDbContext context, DateTime startDate, DateTime endDate) =>
                context.Set<DailyStatus>().Include(b => b.Biometrics).Where(h => h.Visible && h.Date >= startDate && h.Date <= endDate));
    }
}