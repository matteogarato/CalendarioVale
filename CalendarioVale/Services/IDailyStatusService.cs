using CalendarioVale.Data.Model;

namespace CalendarioVale.Services;

public interface IDailyStatusService
{
    public Task<DailyStatus?> GetByDateAndPerson(DateTime date, Person person);

    public Task<List<DailyStatus>> GetBetweenDate(DateTime startDate, DateTime endDate, Person? person = null);

    public Task<List<Biometrics>> GetBiometricsBetweenDate(Person person, DateTime startDate, DateTime endDate, BiometricsType bioType);

    public Task Save(DailyStatus toSave);

    public Task Delete(DateTime date, Person person);

    public Task AddBiometrics(Biometrics bio, Person person);

    public Task DeleteBiometrics(string bioId, DateTime date, Person person);

}