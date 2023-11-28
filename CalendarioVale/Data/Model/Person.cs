using System.ComponentModel.DataAnnotations;

namespace CalendarioVale.Data.Model;

public class Person
{
    [Key]
    public string Id { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
}