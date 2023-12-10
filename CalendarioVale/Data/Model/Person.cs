using System.ComponentModel.DataAnnotations;

namespace CalendarioVale.Data.Model;

public class Person : BaseModel
{
    [Key]
    public string Id { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }

    public string FullName
    {
        get
        {
            return $"{Name} {Surname}";
        }
    }
    public Person(string id, string name, string surname, DateTime birthDate)
    {
        this.Id = id;
        this.Name = name;
        this.Surname = surname;
        this.BirthDate = birthDate;
    }
}