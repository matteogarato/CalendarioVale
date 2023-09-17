using System.Text.Json.Serialization;
using CalendarioVale.Data.Model;

namespace CalendarioVale;

public class BiometricsDto
{
    [JsonPropertyName("value")]
    public int Value { get; set; }
    [JsonPropertyName("type")]
    public BiometricsType Type { get; set; }
    [JsonPropertyName("dateReading")]
    public DateTime? DateReading { get; set; }
    [JsonConstructor]
    public BiometricsDto() { }
}
