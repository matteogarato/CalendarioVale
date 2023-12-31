﻿using System.ComponentModel.DataAnnotations;

namespace CalendarioVale.Data.Model;

public class DailyStatus : BaseModel
{
    [Key]
    public DateTime Date { get; set; }

    [Key]
    public Person Person { get; set; }

    public string Note { get; set; } = string.Empty;

    public DateTime Modify { get; set; }

    public bool Visible { get; set; }

    public ICollection<Biometrics>? Biometrics { get; set; }
}