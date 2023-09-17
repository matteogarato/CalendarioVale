using CalendarioVale.Data.Model;
using CalendarioVale.Data.Reposotory;
using CalendarioVale.Services;
using Moq;

namespace CalendarioVale.Test
{
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
            _mockIDailyStatusRepository.Setup(x => x.GetByDate(It.IsAny<DateTime>())).ReturnsAsync(
                new DailyStatus()
                {
                    Date = DateTime.Now,
                    GotoWork = true,
                    Note = "good day",
                    Modify = DateTime.Now,
                    Visible = true,
                });

            var res = await _service.GetByDate(DateTime.Today);

            Assert.NotNull(res);
            Assert.Equal(DateTime.Today, res.Date);
            Assert.True(res.GotoWork);
            Assert.True(res.Visible);
        }

        [Fact]
        public async void GetBetweenDateShouldReturn()
        {
            _mockIDailyStatusRepository.Setup(x => x.GetBetweenDate(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(
                new List<DailyStatus>()
                {
                    new DailyStatus()
                    {
                        Date = DateTime.Now,
                        GotoWork = true,
                        Note = "good day",
                        Modify = DateTime.Now,
                        Visible = true,
                    },
                    new DailyStatus()
                    {
                        Date = DateTime.Now,
                        GotoWork = true,
                        Note = "good day",
                        Modify = DateTime.Now,
                        Visible = true,
                    },
                    new DailyStatus()
                    {
                        Date = DateTime.Now,
                        GotoWork = true,
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
}