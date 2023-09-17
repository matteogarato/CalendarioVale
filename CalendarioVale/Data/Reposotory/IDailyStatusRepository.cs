using CalendarioVale.Data.Model;

namespace CalendarioVale.Data.Reposotory
{
    public interface IDailyStatusRepository
    {
        Task<DailyStatus> GetByDate(DateTime date);

        IAsyncEnumerable<DailyStatus> GetBetweenDate(DateTime startDate, DateTime endDate);

        Task Save(DailyStatus toSave);
    }
}