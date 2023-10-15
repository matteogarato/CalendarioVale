using System.ComponentModel.DataAnnotations;

namespace CalendarioVale.Data.Model
{
    public class DailyStatus
    {
        [Key]
        public DateTime Date { get; set; }

        public bool GotoWork { get; set; }
        public string Note { get; set; } = string.Empty;

        public DateTime Modify { get; set; }

        public bool Visible { get; set; }

        public ICollection<Biometrics>? Biometrics { get; set; }
    }
}