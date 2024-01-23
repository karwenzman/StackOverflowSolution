using CommunityToolkit.Mvvm.ComponentModel;
using StackOverflow.Library.Models;
using System.ComponentModel.DataAnnotations;

namespace SampleNotifyPropertyChanged.Models;

public partial class PersonDisplayModel : ModelBase
{
    public string FullName
    {
        get
        {
            if (string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(FirstName))
            {
                return $"{LastName}{FirstName}";
            }
            else
            {
                return $"{LastName}, {FirstName}";
            }
        }
    }

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyPropertyChangedFor(nameof(FullName))]
    [Required(AllowEmptyStrings = false, ErrorMessage = "This entry can not be empty!")]
    private string _firstName = string.Empty;
    private string _backupFirstName = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(AllowEmptyStrings = false, ErrorMessage = "This entry can not be empty!")]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string _lastName = string.Empty;
    private string _backupLastName = string.Empty;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "This entry can not be empty!")]
    [Range(1, 999, ErrorMessage = "The age must be in the range of 1 to 999")]
    private int _age;
    private int _backupAge;

    public void GetBackupValues()
    {
        Age = _backupAge;
        FirstName = _backupFirstName;
        LastName = _backupLastName;
    }

    public bool HaveValuesChanged()
    {
        bool output = false;

        if (FirstName != _backupFirstName)
        {
            return true;
        }
        else if (LastName != _backupLastName)
        {
            return true;
        }
        else if (Age != _backupAge)
        {
            return true;
        }

        return output;
    }

    public void MapFrom(PersonModel person)
    {
        _backupFirstName = person.FirstName;
        FirstName = person.FirstName;

        _backupLastName = person.LastName;
        LastName = person.LastName;

        _backupAge = person.Age;
        Age = person.Age;
    }

    public PersonModel MapTo()
    {
        PersonModel output = new()
        {
            FirstName = FirstName,
            LastName = LastName,
            Age = Age
        };

        return output;
    }
}
