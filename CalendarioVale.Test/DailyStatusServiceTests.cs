using CalendarioVale.Data.Model;
using CalendarioVale.Data.Reposotory;
using CalendarioVale.Services;
using Moq;

namespace CalendarioVale.Test;

public class DailyStatusServiceTests
{
    private readonly IDailyStatusService _service;
    private Mock<IDailyStatusRepository> _mockIDailyStatusRepository;

    public DailyStatusServiceTests()
    {
        _mockIDailyStatusRepository = new Mock<IDailyStatusRepository>();
        _service = new DailyStatusService(_mockIDailyStatusRepository.Object);
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
        _mockIDailyStatusRepository.Setup(x => x.GetByDateAndPerson(It.IsAny<DateTime>(), It.IsAny<Person>())).ReturnsAsync(
            testDailyStatus);
        var person = new Person(Guid.NewGuid().ToString(), "name", "person", DateTime.Today);
        var res = await _service.GetByDateAndPerson(DateTime.Today, person);

        Assert.NotNull(res);
        Assert.Equal(testDailyStatus.Note, res.Note);
        Assert.True(res.Visible);
    }

    [Fact]
    public async void GetBetweenDateShouldReturn()
    {
        _mockIDailyStatusRepository.Setup(x => x.GetBetweenDate(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Person>())).Returns(
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
            }.ToAsyncEnumerable()
       );

        var res = await _service.GetBetweenDate(DateTime.Today, DateTime.Today.AddDays(1));

        Assert.NotNull(res);
        Assert.Equal(3, res.Count);
    }

    [Fact]
    public void SaveShouldBeOK()
    {
    }
}