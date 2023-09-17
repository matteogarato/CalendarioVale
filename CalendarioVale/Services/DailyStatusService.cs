using System.Xml.Linq;
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

        public async Task<DailyStatus> GetByDate(DateTime date)
        {
            return await _statusRepository.GetByDate(date);
        }

        public async Task<List<DailyStatus>> GetBetweenDate(DateTime startDate, DateTime endDate)
        {
            return await _statusRepository.GetBetweenDate(startDate, endDate).ToListAsync();
        }

        public async Task Delete(DateTime date)
        {
            var d = await GetByDate(date);
            if (d != null)
            {
                d.Visible = false;
                await Save(d);
            }
        }

        public async Task Save(DailyStatus toSave)
        {
            await _statusRepository.Save(toSave);
        }
        public async Task AddBiometrics(Biometrics bio, DateTime date)
        {
            var d = await GetByDate(date);
            bio.DateReading = DateTime.Now;
            if (d != null)
            {
                d.Biometrics = d.Biometrics is null ? new List<Biometrics>() : d.Biometrics;
                d.Biometrics.Append(bio);
                await Save(d);
            }
        }
        public async Task DeleteBiometrics(string bioId, DateTime date)
        {
            var d = await GetByDate(date);
            if (d != null && d.Biometrics != null && d.Biometrics.Any(b => b.Id == bioId))
            {
                d.Biometrics.Remove(d.Biometrics.Where(b => b.Id == bioId).First());
                await Save(d);
            }
        }
    }
}