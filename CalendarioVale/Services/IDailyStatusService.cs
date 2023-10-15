using CalendarioVale.Data.Model;

namespace CalendarioVale.Services
{
    public interface IDailyStatusService
    {
        public Task<DailyStatus?> GetByDate(DateTime date);

        public Task<List<DailyStatus>> GetBetweenDate(DateTime startDate, DateTime endDate);

        public Task Save(DailyStatus toSave);

        public Task AddBiometrics(Biometrics bio);
    }
}