using StackOverflow.Data.Models;
using System.Diagnostics;
using System.Text;

namespace StackOverflow.Data.Repositories;

public static class PersonDb
{
    public static PersonDataModel GetPerson()
    {
        return new PersonDataModel() { FirstName = "John", LastName = "Smith", Age = 40 };
    }

    public static void SetPerson(PersonDataModel person)
    {
        StringBuilder stringBuilder = new();
        stringBuilder.AppendLine("Faking to save record:");
        stringBuilder.AppendLine($"ID: {person.Id} | Is an active record: {person.IsActive}");
        stringBuilder.Append($"{person.FirstName} {person.LastName}");
        stringBuilder.AppendLine($" | Age: {person.Age}");

        Debug.WriteLine(stringBuilder);
    }
}
