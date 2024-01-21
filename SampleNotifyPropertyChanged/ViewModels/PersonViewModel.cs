using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SampleNotifyPropertyChanged.Models;
using SampleNotifyPropertyChanged.ViewModels.Interfaces;
using System.Diagnostics;

namespace SampleNotifyPropertyChanged.ViewModels;

public partial class PersonViewModel : ViewModelBase, IPersonViewModel
{
    [ObservableProperty]
    private PersonDisplayModel _person = new();

    private PersonDisplayModel _personBackup = new();
    private int _counter = 1;

    public PersonViewModel()
    {
        Person.FirstName = "Jane";
        Person.LastName = "Doe";
        Person.Age = 20;

        _personBackup.FirstName = Person.FirstName;
        _personBackup.LastName = Person.LastName;
        _personBackup.Age = Person.Age;

        // Registering this CommunityToolkit.Mvvm event to enable the call of NotifyCanExecuteChanged().
        Person.PropertyChanged += Person_PropertyChanged;
    }

    /// <summary>
    /// This event handler is finally calling NotifyCanExecuteChanged().
    /// The type PersonDisplayModel is implementing ObservableProperty and OberservableValidation for every property.
    /// Since this class is not aware of the commands in this ViewModel the CanExcecute logic can not called directly
    /// by the model's properties.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Person_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        DiscardButtonCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    public void LoadDataButton()
    {
        // Loading data to a new instance of _personBackup.
        _personBackup = new() { FirstName = "John", LastName = "Smith", Age = 40 };

        // Assigning every property step by step is necessary to keep the same instance of Person, where the event is registered to.
        Person.FirstName = _personBackup.FirstName;
        Person.LastName = _personBackup.LastName;
        Person.Age = _personBackup.Age;
    }

    [RelayCommand(CanExecute = nameof(CanDiscardButton))]
    public void DiscardButton()
    {
        // Assigning every property step by step is necessary to keep the same instance of Person, where the event is registered to.
        Person.FirstName = _personBackup.FirstName;
        Person.LastName = _personBackup.LastName;
        Person.Age = _personBackup.Age;
    }
    public bool CanDiscardButton()
    {
        bool output = false;

        // TODO - How to lower the number of calls for this method?
        Debug.WriteLine($"Passing {nameof(CanDiscardButton)} for {_counter++}x times.");
        //Debug.WriteLine($"HasErrors: {HasErrors}");
        //Debug.WriteLine($"Person.HasErrors: {Person.HasErrors}");

        if (Person.FirstName != _personBackup.FirstName || Person.LastName != _personBackup.LastName || Person.Age != _personBackup.Age)
        {
            output = true;
        }

        return output;
    }

}
