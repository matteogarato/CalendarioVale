using System.Text.Json.Serialization;

namespace CalendarioVale;

public class PersonDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonConstructor]
    public PersonDto()
    { }
}