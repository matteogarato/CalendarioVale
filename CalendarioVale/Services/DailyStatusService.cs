using CalendarioVale.Data.Model;
using CalendarioVale.Data.Reposotory;
using Microsoft.EntityFrameworkCore;

namespace CalendarioVale.Services;

public class DailyStatusService : IDailyStatusService
{
    private readonly IDailyStatusRepository _statusRepository;

    public DailyStatusService(IDailyStatusRepository dailyStatusRepository)
    {
        _statusRepository = dailyStatusRepository;
    }

    public async Task<DailyStatus?> GetByDateAndPerson(DateTime date, Person person)
    {
        return await _statusRepository.GetByDateAndPerson(date, person).ConfigureAwait(false);
    }

    public async Task<List<DailyStatus>> GetBetweenDate(DateTime startDate, DateTime endDate, Person? person = null)
    {
        return await _statusRepository.GetBetweenDate(startDate, endDate, person).ToListAsync().ConfigureAwait(false);
    }

    public async Task Delete(DateTime date, Person person)
    {
        var d = await GetByDateAndPerson(date, person);
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

    public async Task AddBiometrics(Biometrics bio, Person person)
    {
        var d = await GetByDateAndPerson(bio.DateReading, person).ConfigureAwait(false);
        d ??= new DailyStatus
        {
            Date = bio.DateReading,
            Visible = true
        };
        d.Biometrics = d.Biometrics is null ? new List<Biometrics>() : d.Biometrics;
        d.Biometrics.Add(bio);
        await Save(d).ConfigureAwait(false);
    }

    public async Task DeleteBiometrics(string bioId, DateTime date, Person person)
    {
        var d = await GetByDateAndPerson(date, person).ConfigureAwait(false);
        if (d != null && d.Biometrics != null && d.Biometrics.Any(b => b.Id == bioId))
        {
            d.Biometrics.Remove(d.Biometrics.Where(b => b.Id == bioId).First());
            await Save(d).ConfigureAwait(false);
        }
    }

    public async Task<List<Biometrics>> GetBiometricsBetweenDate(Person person, DateTime startDate, DateTime endDate, BiometricsType bioType)
    {
        var res = new List<Biometrics>();
        var dailyStatuses = await GetBetweenDate(startDate, endDate).ConfigureAwait(false);
        if (dailyStatuses is not null && dailyStatuses.Any())
        {
            res.AddRange(dailyStatuses.SelectMany(d => d.Biometrics));
        }
        return res;
    }
}