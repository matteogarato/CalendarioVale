using CalendarioVale.Data.Model;
using CalendarioVale.Data.Reposotory;
using CalendarioVale.Services;

using FluentAssertions;

using NSubstitute;

namespace CalendarioVale.Test;

public class DailyStatusServiceTests
{
    private readonly IDailyStatusService _service;
    private IDailyStatusRepository _IDailyStatusRepository;

    public DailyStatusServiceTests()
    {
        _IDailyStatusRepository = Substitute.For<IDailyStatusRepository>();
        _service = new DailyStatusService(_IDailyStatusRepository);
    }

    [Fact]
    public async void GetByDateShouldReturn()
    {
        var testDailyStatus = new DailyStatus()
        {
            Date = DateTime.Now,
            Note = "good day",
            Modify = DateTime.Now,
            Visible = true,
        };
        _IDailyStatusRepository.GetByDateAndPerson(Arg.Any<DateTime>(), Arg.Any<Person>()).Returns(testDailyStatus);
        var person = new Person(Guid.NewGuid().ToString(), "name", "person", DateTime.Today);
        var res = await _service.GetByDateAndPerson(DateTime.Today, person);

        res.Should().NotBeNull();
        res.Visible.Should().BeTrue();
        res.Note.Should().Be(testDailyStatus.Note);
    }

    [Fact]
    public async void GetBetweenDateShouldReturn()
    {
        var testDailyStatusList =
            new List<DailyStatus>()
            {
                new()
                {
                    Date = DateTime.Now,
                    Note = "good day",
                    Modify = DateTime.Now,
                    Visible = true,
                },
                new()
                {
                    Date = DateTime.Now,
                    Note = "good day",
                    Modify = DateTime.Now,
                    Visible = true,
                },
                new()
                {
                    Date = DateTime.Now,
                    Note = "good day",
                    Modify = DateTime.Now,
                    Visible = true,
                }
            }.ToAsyncEnumerable();
        _IDailyStatusRepository.GetBetweenDate(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<Person>()).Returns(testDailyStatusList);

        var res = await _service.GetBetweenDate(DateTime.Today, DateTime.Today.AddDays(1));

        res.Should().NotBeNull();
        res.Count.Should().Be(3);
    }
}