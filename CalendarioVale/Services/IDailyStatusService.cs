using CalendarioVale.Data.Model;

namespace CalendarioVale.Services;

public interface IDailyStatusService
{
    public Task<DailyStatus?> GetByDate(DateTime date);

    public Task<List<DailyStatus>> GetBetweenDate(DateTime startDate, DateTime endDate);

    public Task<List<Biometrics>> GetBiometricsBetweenDate(DateTime startDate, DateTime endDate, BiometricsType bioType);

    public Task Save(DailyStatus toSave);

    public Task AddBiometrics(Biometrics bio);
}