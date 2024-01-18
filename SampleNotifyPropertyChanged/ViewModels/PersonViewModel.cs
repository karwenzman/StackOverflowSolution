using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SampleNotifyPropertyChanged.ViewModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SampleNotifyPropertyChanged.ViewModels;

public partial class PersonViewModel : ViewModelBase, IPersonViewModel
{
    //public string FirstName { get; set; } = "Jane";
    //public string LastName { get; set; } = "Doe";
    //public int Age { get; set; } = 20;

    public string FullName => $"{LastName}, {FirstName}";

    [ObservableProperty]
    [Required(ErrorMessage = "This entry can not be empty!")]
    [NotifyPropertyChangedFor(nameof(FullName))]
    [NotifyCanExecuteChangedFor(nameof(DiscardButtonCommand))]
    private string _firstName = "Jane";

    [ObservableProperty]
    [Required(ErrorMessage = "This entry can not be empty!")]
    [NotifyPropertyChangedFor(nameof(FullName))]
    [NotifyCanExecuteChangedFor(nameof(DiscardButtonCommand))]
    private string _lastName = "Doe";

    [ObservableProperty]
    [Required(ErrorMessage = "This entry can not be empty!")]
    [Range(1, 999, ErrorMessage = "The age must be in the range of 1 to 999")]
    [NotifyCanExecuteChangedFor(nameof(DiscardButtonCommand))]
    private int _age = 20;


    public PersonViewModel()
    {

    }

    [RelayCommand(CanExecute = nameof(CanLoadDataButton))]
    public void LoadDataButton()
    {
        // TODO - Implement loading data.
    }
    public bool CanLoadDataButton()
    {
        bool output = false;

        return output;
    }

    [RelayCommand(CanExecute = nameof(CanDiscardButton))]
    public void DiscardButton()
    {
        FirstName = "Jane";
        LastName = "Doe";
        Age = 20;
    }
    public bool CanDiscardButton()
    {
        bool output = true;

        // TODO - Why is this not firing?
        if (HasErrors)
        {
            output = false;
        }

        // TODO - If HasErrors is firing this check might not be necessary.
        if (FirstName.Length == 0 || LastName.Length == 0 || Age <= 0)
        {
            output = false;
        }

        return output;
    }
}
