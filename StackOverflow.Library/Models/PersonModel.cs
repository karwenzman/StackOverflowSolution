using StackOverflow.Data.Models;
using StackOverflow.Data.Repositories;

namespace StackOverflow.Library.Models;

public class PersonModel : ModelBase
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }

    public static PersonModel LoadDataFromRepository()
    {
        PersonDataModel personDataModel = new();
        personDataModel = PersonDb.GetPerson();

        PersonModel personModel = new()
        {
            Id = personDataModel.Id,
            FirstName = personDataModel.FirstName,
            LastName = personDataModel.LastName,
            Age = personDataModel.Age,
            IsActive = personDataModel.IsActive,
        };

        return personModel;
    }

    public static void SaveDataToRepository(PersonModel personModel)
    {
        PersonDataModel personDataModel = new()
        {
            Id = personModel.Id,
            FirstName = personModel.FirstName,
            LastName = personModel.LastName,
            Age = personModel.Age,
            IsActive = personModel.IsActive,
        };

        PersonDb.SetPerson(personDataModel);
    }
}
