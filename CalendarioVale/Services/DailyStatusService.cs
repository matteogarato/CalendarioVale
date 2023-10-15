using CalendarioVale.Data.Model;
using CalendarioVale.Data.Reposotory;
using Microsoft.EntityFrameworkCore;

namespace CalendarioVale.Services
{
    public class DailyStatusService : IDailyStatusService
    {
        private readonly IDailyStatusRepository _statusRepository;

        public DailyStatusService(IDailyStatusRepository dailyStatusRepository)
        {
            _statusRepository = dailyStatusRepository;
        }

        public async Task<DailyStatus?> GetByDate(DateTime date)
        {
            return await _statusRepository.GetByDate(date).ConfigureAwait(false);
        }

        public async Task<List<DailyStatus>> GetBetweenDate(DateTime startDate, DateTime endDate)
        {
            return await _statusRepository.GetBetweenDate(startDate, endDate).ToListAsync().ConfigureAwait(false);
        }

        public async Task Delete(DateTime date)
        {
            var d = await GetByDate(date);
            if (d != null)
            {
                d.Visible = false;
                await Save(d).ConfigureAwait(false);
            }
        }

        public async Task Save(DailyStatus toSave)
        {
            await _statusRepository.Save(toSave).ConfigureAwait(false);
        }

        public async Task AddBiometrics(Biometrics bio)
        {
            var d = await GetByDate(bio.DateReading).ConfigureAwait(false);
            d ??= new DailyStatus
            {
                Date = bio.DateReading,
                Visible = true
            };
            d.Biometrics = d.Biometrics is null ? new List<Biometrics>() : d.Biometrics;
            _ = d.Biometrics.Append(bio);
            await Save(d).ConfigureAwait(false);
        }

        public async Task DeleteBiometrics(string bioId, DateTime date)
        {
            var d = await GetByDate(date).ConfigureAwait(false);
            if (d != null && d.Biometrics != null && d.Biometrics.Any(b => b.Id == bioId))
            {
                d.Biometrics.Remove(d.Biometrics.Where(b => b.Id == bioId).First());
                await Save(d).ConfigureAwait(false);
            }
        }
    }
}