using CalendarioVale.Data.Model;

namespace CalendarioVale.Data.Reposotory;

public interface IDailyStatusRepository
{
    Task<DailyStatus?> GetByDateAndPerson(DateTime date, Person person);

    IAsyncEnumerable<DailyStatus> GetBetweenDate(DateTime startDate, DateTime endDate, Person? person = null);

    Task Save(DailyStatus toSave);
}