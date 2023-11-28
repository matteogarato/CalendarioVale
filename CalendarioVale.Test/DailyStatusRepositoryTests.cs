using CalendarioVale.Data.Model;
using CalendarioVale.Data.Reposotory;
using CalendarioVale.Test.Data;

namespace CalendarioVale.Test;

[Collection("Database")]
public class DailyStatusRepositoryTests : DatabaseTestCase
{
    private readonly IDailyStatusRepository _service;

    public DailyStatusRepositoryTests()
        : base()
    {
        _service = new DailyStatusRepository(DbContext);
    }

    [Fact]
    public async void DailyStatusGetByDateshouldNotBeNull()
    {
        var today = new DailyStatus
        {
            Date = DateTime.Today,
            Note = "today was a good day",
            Modify = DateTime.Now
        };

        await DbContext.AddAsync(today);
        await DbContext.SaveChangesAsync();

        var found = await _service.GetByDate(today.Date);

        Assert.NotNull(found);
        Assert.Equal(today.Note, found.Note);
    }

    [Fact]
    public async void DailyStatusGetByMonthshouldNotBeNull()
    {
        var january = new List<DailyStatus>();
        for (int i = 1; i < 11; i++)
        {
            january.Add(new DailyStatus
            {
                Date = new DateTime(2023, 1, i),
                Note = "today was a good day",
                Modify = DateTime.Now
            });
        }

        var february = new List<DailyStatus>();
        for (int i = 1; i < 13; i++)
        {
            february.Add(new DailyStatus
            {
                Date = new DateTime(2023, 2, i),
                Note = "today was a good day",
                Modify = DateTime.Now
            });
        }
        await DbContext.AddRangeAsync(january);
        await DbContext.AddRangeAsync(february);
        await DbContext.SaveChangesAsync();

        var found = await _service.GetBetweenDate(new DateTime(2023, 1, 30), new DateTime(2023, 3, 1)).ToListAsync();

        Assert.NotNull(found);
        Assert.Equal(12, found.Count());
    }
}