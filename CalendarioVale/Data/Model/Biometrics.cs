using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CalendarioVale.Data.Model;

public class Biometrics : BaseModel
{
    [Key]
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("value")]
    public int Value { get; set; }

    [JsonPropertyName("type")]
    public BiometricsType Type { get; set; }

    [JsonPropertyName("dateReading")]
    public DateTime DateReading { get; set; }

    public Biometrics(int value, BiometricsType type, DateTime dateReading)
    {
        Id = Guid.NewGuid().ToString();
        Value = value;
        Type = type;
        DateReading = dateReading;
    }

    public Biometrics()
    {
        Id = Guid.NewGuid().ToString();
    }
}